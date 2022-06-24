namespace Mappers.ServiceEntity;

using Mappers;

public class CompanyMapper : BaseMapper<DTO.ServiceEntity.Company, DTO.RepositoryEntity.Company>
{
    public CompanyMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
    }
}