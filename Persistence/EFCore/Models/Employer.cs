namespace Persistence.Models;

public class Employer
{
    public int Id { get; set; }
    public List<Substitute> Substitutes { get; set; }
    public List<Gig> Gigs { get; set; }
    public List<EmployerSubstitute> EmployerSubstitutes { get; set; }
    
}