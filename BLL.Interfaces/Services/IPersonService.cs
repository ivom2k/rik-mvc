namespace BLL.Interfaces.Services;

public interface IPersonService : IEntityService<DTO.ServiceEntity.Person>
{
    public Task<IEnumerable<DTO.ServiceEntity.Person>> GetAllWithFullNameAsync();

    public Task<DTO.ServiceEntity.Person> GetFirstOrDefaultWithFullNameAsync(Guid id);
}