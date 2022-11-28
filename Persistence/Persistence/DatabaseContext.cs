using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

public class DatabaseContext : DbContext
{

    public DbSet<Employer> Employers { get; set; }
    public DbSet<EmployerEFC> EmployerEfcs { get; set; }
    public DbSet<Substitute> Substitutes { get; set; }
    public DbSet<SubstituteEFC> SubstituteEfcs { get; set; }

    public DbSet<Gig> Gigs { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = VikarTinderData.db");
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employer>().HasKey(employer => employer.Id);
        modelBuilder.Entity<Substitute>().HasKey(substitute => substitute.Id);
        modelBuilder.Entity<Gig>().HasKey(position => position.Id);

    }
}