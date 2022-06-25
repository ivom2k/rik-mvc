namespace Mappers.PublicEntity;
using Mappers;

public class CompanyMapper : BaseMapper<DTO.Public.Company, DTO.ServiceEntity.Company>
{
    public CompanyMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
    }
}