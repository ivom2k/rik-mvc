namespace DTO.ServiceEntity;
using Domain.Base;

public class Person : BaseEntityId
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string PersonalIdentificationCode { get; set; } = default!;

    public Guid PaymentTypeId { get; set; }

    public DTO.ServiceEntity.PaymentType? PaymentType { get; set; }

    public string Notes { get; set; } = default!;

    public ICollection<DTO.ServiceEntity.Participation>? Participations { get; set; }
}