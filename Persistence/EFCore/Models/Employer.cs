using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Employer : User
{
    public List<Substitute> Substitutes { get; set; }
    public List<Gig> Gigs { get; set; }
    public List<EmployerSubstitute> EmployerSubstitutes { get; set; }

    public string Title { get; set; }
    
    public string WorkPlace { get; set; }
}