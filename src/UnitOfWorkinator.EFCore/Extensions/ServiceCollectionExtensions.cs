using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkinator.Abstractions;

namespace UnitOfWorkinator.EFCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUnitOfWorkinator<T>(this IServiceCollection services)
        where T : DbContext
    {
        var repositoryMap = BuildRepositoryMap(services);
        services.AddSingleton(repositoryMap);
        services.AddScoped<IUnitOfWork, UnitOfWork<T>>();
        return services;
    }
    
    private static RepositoryMap BuildRepositoryMap(IServiceCollection services)
    {
        var baseInterface = typeof(IRepository);
        var result = new Dictionary<Type, Type>();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && a.GetName().Name != null);

        var types = assemblies.SelectMany(a => a.GetTypes())
            .Where(type => 
                type is { IsClass: true, IsAbstract: false } && 
                baseInterface.IsAssignableFrom(type));
        
        foreach (var type in types)
        {
            // Find the main interface that it implements in addition to IRepository
            var repoInterface = type.GetInterfaces()
                .FirstOrDefault(i => 
                    i != baseInterface &&
                    baseInterface.IsAssignableFrom(i) &&
                    i != typeof(IDisposable)); 

            if (repoInterface != null)
            {
                result[repoInterface] = type;
            }
        }
        
        // Register the repositories with the DI container
        foreach (var (interfaceType, implementationType) in result)
        {
            services.AddScoped(interfaceType, implementationType);
        }

        return new RepositoryMap(result);
    }
}