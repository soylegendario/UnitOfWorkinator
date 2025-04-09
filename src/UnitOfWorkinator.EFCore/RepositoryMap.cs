namespace UnitOfWorkinator.EFCore;

internal class RepositoryMap(Dictionary<Type, Type> repositoryMap)
{
    public Type GetRepository(Type interfaceType)
    {
        if (repositoryMap.TryGetValue(interfaceType, out var implementationType))
        {
            return implementationType;
        }

        throw new InvalidOperationException($"No repository found for {interfaceType.Name}");
    }
}