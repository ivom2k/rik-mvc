using Domain;
using Mappers.Interfaces;
using Repositories.Interfaces.App;

namespace Repositories.App;

public class CompanyRepository : BaseRepository<DTO.RepositoryEntity.Company, Domain.Models.Company, Domain.ApplicationDbContext>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext dbContext, IBaseMapper<DTO.RepositoryEntity.Company, Domain.Models.Company> mapper) : base(dbContext, mapper)
    {
    }
}