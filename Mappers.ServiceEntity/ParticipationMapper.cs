namespace Mappers.ServiceEntity;

using AutoMapper;
using Mappers;

public class ParticipationMapper : BaseMapper<DTO.ServiceEntity.Participation, DTO.RepositoryEntity.Participation>
{
    public ParticipationMapper(IMapper mapper) : base(mapper)
    {
    }
}