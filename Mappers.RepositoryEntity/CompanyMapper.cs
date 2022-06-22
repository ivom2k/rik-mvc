namespace Mappers.RepositoryEntity;
using Mappers;
using DTO.RepositoryEntity;
using Domain;

public class CompanyMapper : BaseMapper<DTO.RepositoryEntity.Company, Domain.Models.Company>
{
    public CompanyMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
        
    }
}