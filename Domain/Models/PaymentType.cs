public class PaymentType : IBaseEntityId {

    public string Type { get; set; } = default!;

    public ICollection<Person>? Persons { get; set; }

    public ICollection<Company>? Companies { get; set; }
}