namespace Repositories.Interfaces;
using Domain.Interfaces;

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid> where TEntity : class, IBaseEntityId
{

}

public interface IBaseRepository<TEntity, TKey> where TEntity : class, IBaseEntityId<TKey> where TKey : IEquatable<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> FirstOrDefaultAsync(TKey id);

    Task<TEntity> RemoveAsync(TKey id);

    Task<bool> ExistsAsync(TKey id);

    TEntity Add(TEntity entity);
    
    TEntity Update(TEntity entity);
    
    TEntity Remove(TEntity entity);
    
    TEntity Remove(TKey id);

    TEntity FirstOrDefault(TKey id);
}