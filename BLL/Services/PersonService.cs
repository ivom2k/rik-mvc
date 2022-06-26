namespace BLL.Services;
using Repositories.Interfaces.App;
using BLL.Interfaces.Services;
using Mappers.Interfaces;

public class PersonService : BaseEntityService<DTO.ServiceEntity.Person, DTO.RepositoryEntity.Person, IPersonRepository>, IPersonService
{
    public PersonService(IPersonRepository repository, IBaseMapper<DTO.ServiceEntity.Person, DTO.RepositoryEntity.Person> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<DTO.ServiceEntity.Person>> GetAllWithFullNameAsync()
    {
        var result = (await Repository.GetAllAsync()).Select(e => Mapper.Map(e)).ToList();

        foreach (var person in result)
        {
            if (person != null)
            person.FullName = $"{person.FirstName} {person.LastName}";
        }

        return result;
    }

    public async Task<DTO.ServiceEntity.Person> GetFirstOrDefaultWithFullNameAsync(Guid id)
    {
        var result = Mapper.Map((await Repository.FirstOrDefaultAsync(id)));

        result.FullName = $"{result.FirstName} {result.LastName}";

        return result;
    }

}