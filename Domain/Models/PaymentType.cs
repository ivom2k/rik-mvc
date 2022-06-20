namespace Domain.Models;
using Domain.Base;

public class PaymentType : BaseEntityId {

    public string Type { get; set; } = default!;

    public ICollection<Person>? Persons { get; set; }

    public ICollection<Company>? Companies { get; set; }
}