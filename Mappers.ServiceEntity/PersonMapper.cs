namespace Mappers.ServiceEntity;

using AutoMapper;
using Mappers;

public class PersonMapper : BaseMapper<DTO.ServiceEntity.Person, DTO.RepositoryEntity.Person>
{
    public PersonMapper(IMapper mapper) : base(mapper)
    {
    }
}