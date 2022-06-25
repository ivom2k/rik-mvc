namespace DTO.ServiceEntity;
using Domain.Base;

public class PaymentType : BaseEntityId
{
    public string Type { get; set; } = default!;

    public ICollection<DTO.ServiceEntity.Participation>? Participations { get; set; }
}