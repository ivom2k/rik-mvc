public class Participation : IBaseEntityId {

    public Guid EventId { get; set; }
    public Event Event { get; set; }

    public Guid? PersonId { get; set; }

    public Person? Person { get; set; }

    public Guid? CompanyId { get; set; }

    public Company? Company { get; set; }
}