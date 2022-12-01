using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Models;

namespace Persistence;

public class DataContext : DbContext
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages{ get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<JobConfirmation> JobConfirmations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Data.db");
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