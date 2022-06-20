namespace Domain;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext {

    public DbSet<Models.Company> Companies { get; set; } = default!;

    public DbSet<Models.Event> Events { get; set; } = default!;

    public DbSet<Models.Participation> Participations { get; set; } = default!;

    public DbSet<Models.PaymentType> PaymentTypes { get; set; } = default!;
    
    public DbSet<Models.Person> Persons { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}