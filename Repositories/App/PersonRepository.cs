using Domain;
using Mappers.Interfaces;
using Repositories.Interfaces.App;

namespace Repositories.App;

public class PersonRepository : BaseRepository<DTO.RepositoryEntity.Person, Domain.Models.Person, Domain.ApplicationDbContext>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext dbContext, IBaseMapper<DTO.RepositoryEntity.Person, Domain.Models.Person> mapper) : base(dbContext, mapper)
    {
    }
}