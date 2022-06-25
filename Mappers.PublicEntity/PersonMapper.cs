namespace Mappers.PublicEntity;

using AutoMapper;
using Mappers;

public class PersonMapper : BaseMapper<DTO.Public.Person, DTO.ServiceEntity.Person>
{
    public PersonMapper(IMapper mapper) : base(mapper)
    {
    }
}