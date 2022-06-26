namespace DTO.Public;
using Domain.Base;

public class Event : BaseEntityId
{
    public string Name { get; set; } = default!;
    
    public DateTime StartTime { get; set; }

    public string Location { get; set; } = default!;

    public string Notes { get; set; } = default!;

    public int? TotalParticipants { get; set; }

    public ICollection<DTO.Public.Participation>? Participations { get; set; }
}