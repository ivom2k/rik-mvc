namespace UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();

    int SaveChanges();
}