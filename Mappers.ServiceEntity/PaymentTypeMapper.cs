namespace Mappers.ServiceEntity;

using AutoMapper;
using Mappers;

public class PaymentTypeMapper : BaseMapper<DTO.ServiceEntity.PaymentType, DTO.RepositoryEntity.PaymentType>
{
    public PaymentTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}