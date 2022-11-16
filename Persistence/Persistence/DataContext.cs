using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

public class DataContext : DbContext
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages{ get; set; }
    public DbSet<User> Users { get; set; }

    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Data.db");
    }
}