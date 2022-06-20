namespace DTO.RepositoryEntity;
using Domain.Base;
public class Event : BaseEntityId
{
    public string Name { get; set; } = default!;
    
    public DateTime StartTime { get; set; }

    public string Location { get; set; } = default!;

    public string Notes { get; set; } = default!;

    public ICollection<Participation>? Participations { get; set; }
}