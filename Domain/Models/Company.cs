namespace Domain.Models;

using System.ComponentModel.DataAnnotations;
using Domain.Base;

public class Company : BaseEntityId {

    public string Name { get; set; } = default!;

    public int Code { get; set; }

    public int ParticipantsCount { get; set; }

    [MaxLength(5000)]
    public string? Notes { get; set; } = default!;

    public ICollection<Participation>? Participations { get; set; }
}