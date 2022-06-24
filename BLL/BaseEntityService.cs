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
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await Repository.ExistsAsync(id);
    }

    public TServiceEntity FirstOrDefault(TKey id)
    {
        return Mapper.Map(Repository.FirstOrDefault(id)!)!;
    }

    public async Task<TServiceEntity> FirstOrDefaultAsync(TKey id)
    {
        return Mapper.Map((await Repository.FirstOrDefaultAsync(id)!))!;
    }

    public async Task<IEnumerable<TServiceEntity>> GetAllAsync()
    {
        return (await Repository.GetAllAsync()).Select(e => Mapper.Map(e)!);
    }

    public TServiceEntity Remove(TServiceEntity entity)
    {
        return Mapper.Map(Repository.Remove(Mapper.Map(entity)!))!;
    }

    public TServiceEntity Remove(TKey id)
    {
        return Mapper.Map(Repository.Remove(id))!;
    }

    public async Task<TServiceEntity> RemoveAsync(TKey id)
    {
        return Mapper.Map(await Repository.RemoveAsync(id))!;
    }

    public TServiceEntity Update(TServiceEntity entity)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!))!;
    }
}