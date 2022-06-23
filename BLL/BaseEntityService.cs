namespace BLL;
using BLL.Interfaces;
using Domain.Interfaces;
using Repositories.Interfaces;
using Mappers.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BaseEntityService<TServiceEntity, TRepositoryEntity, TRepository> : BaseEntityService<TServiceEntity, TRepositoryEntity, TRepository, Guid>, IEntityService<TServiceEntity>
where TRepositoryEntity : class, IBaseEntityId
where TServiceEntity : class, IBaseEntityId
where TRepository : IBaseRepository<TRepositoryEntity>
{
    public BaseEntityService(TRepository repository, IBaseMapper<TServiceEntity, TRepositoryEntity> mapper) : base(repository, mapper)
    {
        
    }
}

public class BaseEntityService<TServiceEntity, TRepositoryEntity, TRepository, TKey> : IEntityService<TServiceEntity, TKey>
where TKey : IEquatable<TKey>
where TServiceEntity : class, IBaseEntityId<TKey>
where TRepository : IBaseRepository<TRepositoryEntity, TKey>
where TRepositoryEntity : class, IBaseEntityId<TKey>
{

    protected TRepository Repository;
    protected IBaseMapper<TServiceEntity, TRepositoryEntity> Mapper;

    public BaseEntityService(TRepository repository, IBaseMapper<TServiceEntity, TRepositoryEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public TServiceEntity Add(TServiceEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public TServiceEntity FirstOrDefault(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<TServiceEntity> FirstOrDefaultAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TServiceEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public TServiceEntity Remove(TServiceEntity entity)
    {
        throw new NotImplementedException();
    }

    public TServiceEntity Remove(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<TServiceEntity> RemoveAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public TServiceEntity Update(TServiceEntity entity)
    {
        throw new NotImplementedException();
    }
}