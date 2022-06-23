namespace DTO.ServiceEntity;
using Domain.Base;

public class Company : BaseEntityId
{
    public string Name { get; set; } = default!;

    public int Code { get; set; }

    public int ParticipantsCount { get; set; }

    public Guid PaymentTypeId { get; set; }

    public DTO.ServiceEntity.PaymentType? PaymentType { get; set; }

    public string? Notes { get; set; } = default!;

    public ICollection<DTO.ServiceEntity.Participation>? Participations { get; set; }
}