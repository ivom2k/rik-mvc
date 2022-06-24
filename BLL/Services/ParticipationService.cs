namespace BLL.Services;
using Repositories.Interfaces.App;
using BLL.Interfaces.Services;
using Mappers.Interfaces;

public class ParticipationService : BaseEntityService<DTO.ServiceEntity.Participation, DTO.RepositoryEntity.Participation, IParticipationRepository>, IParticipationService
{
    public ParticipationService(IParticipationRepository repository, IBaseMapper<DTO.ServiceEntity.Participation, DTO.RepositoryEntity.Participation> mapper) : base(repository, mapper)
    {
    }
}