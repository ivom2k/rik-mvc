namespace Mappers.RepositoryEntity;
using Mappers;
using DTO.RepositoryEntity;
using Domain;

public class PersonTypeMapper : BaseMapper<DTO.RepositoryEntity.Person, Domain.Models.Person>
{
    public PersonTypeMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
        
    }
}