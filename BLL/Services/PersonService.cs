namespace BLL.Services;
using Repositories.Interfaces.App;
using BLL.Interfaces.Services;
using Mappers.Interfaces;

public class PersonService : BaseEntityService<DTO.ServiceEntity.Person, DTO.RepositoryEntity.Person, IPersonRepository>, IPersonService
{
    public PersonService(IPersonRepository repository, IBaseMapper<DTO.ServiceEntity.Person, DTO.RepositoryEntity.Person> mapper) : base(repository, mapper)
    {
    }
}