namespace UnitOfWork;
using UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BaseUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    protected readonly TDbContext UnitOfWorkDbContext;

    public BaseUnitOfWork(TDbContext dbContext)
    {
        UnitOfWorkDbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await UnitOfWorkDbContext.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return UnitOfWorkDbContext.SaveChanges();
    }

}