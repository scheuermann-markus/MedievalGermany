using Microsoft.EntityFrameworkCore;
using MedievalGermany.Domain.Models;

namespace MedievalGermany.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Define "Castles" table
    public DbSet<Castle> Castles { get; set; }

    // Mark "Geolocation" as owned by "Castle"
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Castle>()
            .OwnsOne(c => c.Geolocation);
    }
}