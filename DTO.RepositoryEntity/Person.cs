namespace DTO.RepositoryEntity;
using Domain.Base;

public class Person : BaseEntityId
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string PersonalIdentificationCode { get; set; } = default!;

    public string Notes { get; set; } = default!;

    public ICollection<DTO.RepositoryEntity.Participation>? Participations { get; set; }
}