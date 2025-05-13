using Microsoft.EntityFrameworkCore;
using UnitOfWorkinator.Abstractions;

namespace UnitOfWorkinator.EFCore;

public abstract class BaseRepository(DbContext context): IRepository
{
    protected DbContext Context = context;
}