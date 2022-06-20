namespace DTO.RepositoryEntity;
using Domain.Base;
public class Company : BaseEntityId
{
    public string Name { get; set; } = default!;

    public int Code { get; set; }

    public int ParticipantsCount { get; set; }

    public Guid PaymentTypeId { get; set; }

    public PaymentType? PaymentType { get; set; }

    public string? Notes { get; set; } = default!;

    public ICollection<Participation>? Participations { get; set; }
}
