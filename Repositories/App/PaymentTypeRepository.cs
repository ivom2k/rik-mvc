using Domain;
using Mappers.Interfaces;
using Repositories.Interfaces.App;

namespace Repositories.App;

public class PaymentTypeRepository : BaseRepository<DTO.RepositoryEntity.PaymentType, Domain.Models.PaymentType, Domain.ApplicationDbContext>, IPaymentTypeRepository
{
    public PaymentTypeRepository(ApplicationDbContext dbContext, IBaseMapper<DTO.RepositoryEntity.PaymentType, Domain.Models.PaymentType> mapper) : base(dbContext, mapper)
    {
    }
}