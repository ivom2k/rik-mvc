namespace Mappers.PublicEntity;

using AutoMapper;
using Mappers;

public class PaymentTypeMapper : BaseMapper<DTO.Public.PaymentType, DTO.ServiceEntity.PaymentType>
{
    public PaymentTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}