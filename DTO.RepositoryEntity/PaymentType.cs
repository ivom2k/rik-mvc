namespace DTO.RepositoryEntity;
using Domain.Base;

public class PaymentType : BaseEntityId
{
    public string Type { get; set; } = default!;

    public ICollection<DTO.RepositoryEntity.Person>? Persons { get; set; }

    public ICollection<DTO.RepositoryEntity.Company>? Companies { get; set; }
}