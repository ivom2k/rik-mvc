namespace Mappers.PublicEntity;

using AutoMapper;
using Mappers;

public class ParticipationMapper : BaseMapper<DTO.Public.Participation, DTO.ServiceEntity.Participation>
{
    public ParticipationMapper(IMapper mapper) : base(mapper)
    {
    }
}