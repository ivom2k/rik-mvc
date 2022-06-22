using Domain.Interfaces;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mappers.Interfaces;

namespace Repositories;

public class BaseRepository<TRepositoryEntity, TDomainEntity, TDbContext> : BaseRepository<TRepositoryEntity, TDomainEntity, Guid, TDbContext>
where TRepositoryEntity : class, IBaseEntityId<Guid>
where TDomainEntity : class, IBaseEntityId<Guid>
where TDbContext : DbContext
{
    public BaseRepository(TDbContext dbContext, IBaseMapper<TRepositoryEntity, TDomainEntity> mapper) : base(dbContext, mapper)
    {
        
    }
}

public class BaseRepository<TRepositoryEntity, TDomainEntity, TKey, TDbContext> : IBaseRepository<TRepositoryEntity, TKey>
where TRepositoryEntity : class, IBaseEntityId<TKey>
where TDomainEntity : class, IBaseEntityId<TKey>
where TKey : IEquatable<TKey>
where TDbContext : DbContext
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IBaseMapper<TRepositoryEntity, TDomainEntity> Mapper;

    public BaseRepository(TDbContext dbContext, IBaseMapper<TRepositoryEntity, TDomainEntity> mapper)
    {
        RepoDbContext = dbContext;
        Mapper = mapper;
        RepoDbSet = dbContext.Set<TDomainEntity>();
    }

    public virtual TRepositoryEntity Add(TRepositoryEntity entity)
    {
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public async virtual Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));
    }

    public TRepositoryEntity FirstOrDefault(TKey id)
    {
        return Mapper.Map(RepoDbSet.FirstOrDefault(e => e.Id.Equals(id)))!;
    }

    public async virtual Task<TRepositoryEntity> FirstOrDefaultAsync(TKey id)
    {
        return Mapper.Map(await RepoDbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id)))!;
    }

    public async virtual Task<IEnumerable<TRepositoryEntity>> GetAllAsync()
    {
        return (await RepoDbSet.AsNoTracking().ToListAsync()).Select(e => Mapper.Map(e)!);
    }

    public virtual TRepositoryEntity Remove(TRepositoryEntity entity)
    {
        return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TRepositoryEntity Remove(TKey id)
    {
        var entity = FirstOrDefault(id);

        if (entity == null) {
            throw new NullReferenceException($"Entity {typeof(TRepositoryEntity).Name} with id {id} was not found");
        }

        return Remove(entity);
    }

    public virtual async Task<TRepositoryEntity> RemoveAsync(TKey id)
    {
        var entity = await FirstOrDefaultAsync(id);

        if (entity == null) {
            throw new NullReferenceException($"Entity {typeof(TRepositoryEntity).Name} with id {id} was not found");
        }

        return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TRepositoryEntity Update(TRepositoryEntity entity)
    {
        return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }
}