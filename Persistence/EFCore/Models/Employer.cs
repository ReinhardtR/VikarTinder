namespace Persistence.Models;

public class Employer
{
    public int Id { get; set; }
    public List<Substitute> Substitutes { get; set; }
    public List<Gig> Gigs { get; set; }
    public List<EmployerSubstitute> EmployerSubstitutes { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string Title { get; set; }
    
    public string WorkPlace { get; set; }
}