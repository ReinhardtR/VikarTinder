using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Substitute> Substitutes { get; set; }
    public DbSet<Gig> WorkPosition { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = VikarTinder.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employer>().HasKey(employer => employer.Id);
        modelBuilder.Entity<Substitute>().HasKey(substitute => substitute.Id);
        modelBuilder.Entity<Gig>().HasKey(position => position.Id);

    }
}