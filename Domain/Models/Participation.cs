namespace Domain.Models;
using Domain.Base;

public class Participation : BaseEntityId {

    public Guid EventId { get; set; }
    public Event? Event { get; set; }

    public Guid? PersonId { get; set; }

    public Person? Person { get; set; }

    public Guid? CompanyId { get; set; }

    public Company? Company { get; set; }

    public Guid PaymentTypeId { get; set; }

    public PaymentType? PaymentType { get; set; }
}