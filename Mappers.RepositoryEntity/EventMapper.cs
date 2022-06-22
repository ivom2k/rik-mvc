namespace Mappers.RepositoryEntity;
using Mappers;
using DTO.RepositoryEntity;
using Domain;

public class EventMapper : BaseMapper<DTO.RepositoryEntity.Event, Domain.Models.Event>
{
    public EventMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
        
    }
}