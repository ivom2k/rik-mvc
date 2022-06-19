public class Event : IBaseEntityId {

    public string Name { get; set; } = default!;
    
    public DateTime StartTime { get; set; }

    public string Location { get; set; } = default!;

    public string Notes { get; set; } = default!;

    public ICollection<Participation>? Participations { get; set; }

}