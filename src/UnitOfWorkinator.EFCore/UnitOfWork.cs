using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkinator.Abstractions;

namespace UnitOfWorkinator.EFCore;

internal class UnitOfWork<TContext>(TContext context, RepositoryMap repositoryMap) : IUnitOfWork where TContext : DbContext
{
    private readonly ConcurrentDictionary<Type, IRepository> _repositories = new();

    /// <inheritdoc />
    public T Repository<T>() where T : IRepository
    {
        return (T)_repositories.GetOrAdd(typeof(T), _ =>
        {
            var implementation = repositoryMap.GetRepository(typeof(T));
            var repository = (T)Activator.CreateInstance(implementation, context)!;
            return repository ?? throw new InvalidOperationException($"Could not create instance of {typeof(T).Name}");
        });    
    }

    /// <inheritdoc />
    public Task SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
}