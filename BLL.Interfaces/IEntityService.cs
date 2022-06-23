namespace BLL.Interfaces;
using Repositories.Interfaces;
using Domain.Interfaces;

public interface IEntityService<TEntity> : IBaseRepository<TEntity>, IEntityService<TEntity, Guid>
where TEntity : class, IBaseEntityId
{
    
}

public interface IEntityService<TEntity, TKey> : IBaseRepository<TEntity, TKey>
where TKey : IEquatable<TKey>
where TEntity : class, IBaseEntityId<TKey>
{

}