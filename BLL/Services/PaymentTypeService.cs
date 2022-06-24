namespace BLL.Services;
using Repositories.Interfaces.App;
using BLL.Interfaces.Services;
using Mappers.Interfaces;

public class PaymentTypeService : BaseEntityService<DTO.ServiceEntity.PaymentType, DTO.RepositoryEntity.PaymentType, IPaymentTypeRepository>, IPaymentTypeService
{
    public PaymentTypeService(IPaymentTypeRepository repository, IBaseMapper<DTO.ServiceEntity.PaymentType, DTO.RepositoryEntity.PaymentType> mapper) : base(repository, mapper)
    {
    }
}