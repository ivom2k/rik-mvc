namespace DTO.ServiceEntity;
using Domain.Base;

public class Participation : BaseEntityId
{
    public Guid EventId { get; set; }
    
    public Event? Event { get; set; }

    public Guid? PersonId { get; set; }

    public DTO.ServiceEntity.Person? Person { get; set; }

    public Guid? CompanyId { get; set; }

    public DTO.ServiceEntity.Company? Company { get; set; }
}
