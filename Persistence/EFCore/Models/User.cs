using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public abstract class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    public string Salt { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
}