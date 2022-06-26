namespace DTO.Public;
using Domain.Base;

public class Person : BaseEntityId
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string PersonalIdentificationCode { get; set; } = default!;

    public string Notes { get; set; } = default!;

    public string? FullName { get; set; } = default!;

    public ICollection<DTO.Public.Participation>? Participations { get; set; }
}