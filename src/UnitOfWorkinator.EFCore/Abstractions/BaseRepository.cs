using Microsoft.EntityFrameworkCore;
using UnitOfWorkinator.Abstractions;

namespace UnitOfWorkinator.EFCore;

internal abstract class BaseRepository(DbContext context): IRepository
{
    protected DbContext Context = context;
}