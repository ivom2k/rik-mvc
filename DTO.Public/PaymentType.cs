namespace DTO.Public;
using Domain.Base;

public class PaymentType : BaseEntityId
{
    public string Type { get; set; } = default!;

    public ICollection<DTO.Public.Participation>? Participations { get; set; }
}