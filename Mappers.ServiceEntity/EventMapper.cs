namespace Mappers.ServiceEntity;

using AutoMapper;
using Mappers;

public class EventMapper : BaseMapper<DTO.ServiceEntity.Event, DTO.RepositoryEntity.Event>
{
    public EventMapper(IMapper mapper) : base(mapper)
    {
    }
}