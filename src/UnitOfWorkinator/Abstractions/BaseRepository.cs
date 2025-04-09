using Microsoft.EntityFrameworkCore;

namespace UnitOfWorkinator;

internal abstract class BaseRepository(DbContext context): IRepository
{
    protected DbContext Context = context;
}