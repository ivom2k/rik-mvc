namespace DTO.Public;
using Domain.Base;

public class Participation : BaseEntityId
{
    public Guid EventId { get; set; }
    
    public Event? Event { get; set; }

    public Guid? PersonId { get; set; }

    public DTO.Public.Person? Person { get; set; }

    public Guid? CompanyId { get; set; }

    public DTO.Public.Company? Company { get; set; }

    public Guid PaymentTypeId { get; set; }

    public DTO.Public.PaymentType? PaymentType { get; set; }
}