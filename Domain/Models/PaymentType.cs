namespace Domain.Models;
using Domain.Base;

public class PaymentType : BaseEntityId {

    public string Type { get; set; } = default!;

    public ICollection<Participation>? Participations { get; set; }
}