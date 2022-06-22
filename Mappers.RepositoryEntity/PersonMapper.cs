namespace Mappers.RepositoryEntity;
using Mappers;


public class PersonMapper : BaseMapper<DTO.RepositoryEntity.Person, Domain.Models.Person>
{
    public PersonMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
        
    }
}