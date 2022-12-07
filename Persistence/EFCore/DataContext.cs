using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Models;

namespace Persistence;

public class DataContext : DbContext
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages{ get; set; }
    public DbSet<User> Users { get; set; }
    
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Substitute> Substitutes { get; set; }
    public DbSet<Gig> Gigs { get; set; }

    public DbSet<JobConfirmation> JobConfirmations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EFCore/Data.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Explicit many to many relationships
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
        
        modelBuilder.Entity<Employer>()
            .HasMany(p => p.Substitutes)
            .WithMany(p => p.Employers)
            .UsingEntity<EmployerSubstitute>(
                j => j
                    .HasOne(pt => pt.Substitute)
                    .WithMany(t => t.EmployerSubstitutes)
                    .HasForeignKey(pt => pt.SubstituteId),
                j => j
                    .HasOne(pt => pt.Employer)
                    .WithMany(p => p.EmployerSubstitutes)
                    .HasForeignKey(pt => pt.EmployerId),
                j =>
                {
                    j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.EmployerId, t.SubstituteId });
                });
        
        // Required relationships
        modelBuilder.Entity<Employer>()
            .HasMany((e) => e.Gigs)
            .WithOne((g) => g.Employer)
            .IsRequired();
    }
    
    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void AddTimestamps()
    {
        IEnumerable<EntityEntry> entities = ChangeTracker.Entries()
            .Where(x => x.Entity is DateTrackingEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (EntityEntry entity in entities)
        {
            DateTime now = DateTime.UtcNow; // current datetime
            
            DateTrackingEntity dateTrackingEntity = (DateTrackingEntity) entity.Entity;
            
            if (entity.State == EntityState.Added)
            {
                dateTrackingEntity.CreatedAt = now;
            }
            
            dateTrackingEntity.UpdatedAt = now;
        }
    }
    
}