using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

public class DatabaseContext : DbContext
{

    public DbSet<Employer> Employers { get; set; }
    public DbSet<Substitute> Substitutes { get; set; }
    public DbSet<Gig> Gigs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = VikarTinder.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Substitute>()
            .HasMany(p => p.Positions)
            .WithMany(p => p.Substitutes)
            .UsingEntity<GigSubstitute>(
                j => j
                    .HasOne(pt => pt.Gig)
                    .WithMany(t => t.GigSubstitutes)
                    .HasForeignKey(pt => pt.GigId),
                j => j
                    .HasOne(pt => pt.Substitute)
                    .WithMany(p => p.GigSubstitutes)
                    .HasForeignKey(pt => pt.SubstituteId),
                j =>
                {
                    j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.SubstituteId, t.GigId });
                });
    }
}