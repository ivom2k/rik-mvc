public class ApplicaitonDbContext {

    public DbSet<Company> Companies { get; set; } = default!;

    public DbSet<Event> Events { get; set; } = default!;

    public DbSet<Participation> Participations { get; set; } = default!;

    public DbSet<PaymentType> PaymentTypes { get; set; } = default!;
    
    public DbSet<Person> Persons { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

}