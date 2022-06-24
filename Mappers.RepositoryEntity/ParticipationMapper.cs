namespace Mappers.RepositoryEntity;
using Mappers;

public class ParticipationMapper : BaseMapper<DTO.RepositoryEntity.Participation, Domain.Models.Participation>
{
    public ParticipationMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
        
    }
}