namespace Mappers.RepositoryEntity;
using Mappers;
using DTO.RepositoryEntity;
using Domain;

public class PaymentTypeMapper : BaseMapper<DTO.RepositoryEntity.PaymentType, Domain.Models.PaymentType>
{
    public PaymentTypeMapper(AutoMapper.IMapper mapper) : base(mapper)
    {
        
    }
}