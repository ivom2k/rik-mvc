namespace Domain.Models;

using System.ComponentModel.DataAnnotations;
using Domain.Base;

public class Person : BaseEntityId {

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string PersonalIdentificationCode { get; set; } = default!;

    [MaxLength(1500)]
    public string? Notes { get; set; } = default!;

    public ICollection<Participation>? Participations { get; set; }
}