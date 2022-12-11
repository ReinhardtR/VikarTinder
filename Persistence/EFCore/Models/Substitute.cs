using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Substitute : User
{
    public int Age { get; set; }
    public string Bio { get; set; }
    public string Address { get; set; }
    
    public ICollection<Gig> Positions { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }
    public List<Employer> Employers { get; set; }
    public List<EmployerSubstitute> EmployerSubstitutes { get; set; }

    // TODO: why initialize lists here?
    public Substitute()
    {
        Positions = new List<Gig>();
        Employers = new List<Employer>();
        GigSubstitutes = new List<GigSubstitute>();
        EmployerSubstitutes = new List<EmployerSubstitute>();
    }
}