namespace DTO.ServiceEntity;
using Domain.Base;

public class PaymentType : BaseEntityId
{
    public string Type { get; set; } = default!;

    public ICollection<DTO.ServiceEntity.Person>? Persons { get; set; }

    public ICollection<DTO.ServiceEntity.Company>? Companies { get; set; }
}