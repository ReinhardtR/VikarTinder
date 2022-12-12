using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Gig
{
    [Key]
    public int Id { get; set; }
    
    public int EmployerId { get; set; }
    public Employer Employer { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }
    public List<Substitute> Substitutes { get; set; }
}