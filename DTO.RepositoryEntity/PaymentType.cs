namespace DTO.RepositoryEntity;
using Domain.Base;

public class PaymentType : BaseEntityId
{
    public string Type { get; set; } = default!;

    public ICollection<DTO.RepositoryEntity.Participation>? Participations { get; set; }
}