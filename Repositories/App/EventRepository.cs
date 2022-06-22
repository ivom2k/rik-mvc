using Domain;
using Mappers.Interfaces;
using Repositories.Interfaces.App;

namespace Repositories.App;

public class EventRepository : BaseRepository<DTO.RepositoryEntity.Event, Domain.Models.Event, Domain.ApplicationDbContext>, IEventRepository
{
    public EventRepository(ApplicationDbContext dbContext, IBaseMapper<DTO.RepositoryEntity.Event, Domain.Models.Event> mapper) : base(dbContext, mapper)
    {
    }
}