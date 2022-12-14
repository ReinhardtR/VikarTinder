using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Substitute : User
{
    private DateTime _birthDate;
    
    public DateTime BirthDate {
        get => _birthDate.ToUniversalTime();
        set => _birthDate = value.ToUniversalTime();
    }
    
    public string Bio { get; set; }
    public string Address { get; set; }
    
    public ICollection<Gig> Gigs { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }
    public List<Employer> Employers { get; set; }
    public List<EmployerSubstitute> EmployerSubstitutes { get; set; }

    // // TODO: why initialize lists here?
    // public Substitute()
    // {
    //     Gigs = new List<Gig>();
    //     Employers = new List<Employer>();
    //     GigSubstitutes = new List<GigSubstitute>();
    //     EmployerSubstitutes = new List<EmployerSubstitute>();
    // }
}