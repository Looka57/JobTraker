using Microsoft.EntityFrameworkCore;
using JobTracker.Domain.Entities;

namespace JobTracker.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Candidature> Candidatures { get; set; }
    public DbSet<Company> Compagny { get; set; }
    public DbSet<Interaction> Interactions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Candidature>()
            .Property(c => c.Salaire)
            .HasPrecision(18, 2);
    }
}