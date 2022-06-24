namespace BLL.Services;
using Repositories.Interfaces.App;
using BLL.Interfaces.Services;
using Mappers.Interfaces;

public class CompanyService : BaseEntityService<DTO.ServiceEntity.Company, DTO.RepositoryEntity.Company, ICompanyRepository>, ICompanyService
{
    public CompanyService(ICompanyRepository repository, IBaseMapper<DTO.ServiceEntity.Company, DTO.RepositoryEntity.Company> mapper) : base(repository, mapper)
    {
    }
}