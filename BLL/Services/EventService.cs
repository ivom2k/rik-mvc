namespace BLL.Services;
using Repositories.Interfaces.App;
using BLL.Interfaces.Services;
using Mappers.Interfaces;

public class EventService : BaseEntityService<DTO.ServiceEntity.Event, DTO.RepositoryEntity.Event, IEventRepository>, IEventService
{
    public EventService(IEventRepository repository, IBaseMapper<DTO.ServiceEntity.Event, DTO.RepositoryEntity.Event> mapper) : base(repository, mapper)
    {
    }
}