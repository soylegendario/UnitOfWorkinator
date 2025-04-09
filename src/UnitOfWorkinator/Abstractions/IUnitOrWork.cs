namespace UnitOfWorkinator;

public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    T Repository<T>() where T : IRepository;
    
    /// <summary>
    /// Save the current changes in the context
    /// </summary>
    Task SaveChangesAsync();
}