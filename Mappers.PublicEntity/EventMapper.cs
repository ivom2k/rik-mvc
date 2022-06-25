namespace Mappers.PublicEntity;

using AutoMapper;
using Mappers;

public class EventMapper : BaseMapper<DTO.Public.Event, DTO.ServiceEntity.Event>
{
    public EventMapper(IMapper mapper) : base(mapper)
    {
    }
}