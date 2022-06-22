using Domain;
using Mappers.Interfaces;
using Repositories.Interfaces.App;

namespace Repositories.App;

public class ParticipationRepository : BaseRepository<DTO.RepositoryEntity.Participation, Domain.Models.Participation, Domain.ApplicationDbContext>, IParticipationRepository
{
    public ParticipationRepository(ApplicationDbContext dbContext, IBaseMapper<DTO.RepositoryEntity.Participation, Domain.Models.Participation> mapper) : base(dbContext, mapper)
    {
    }
}