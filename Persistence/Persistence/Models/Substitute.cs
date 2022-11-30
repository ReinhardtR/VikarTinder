namespace Persistence.Models;

public class Substitute
{
    public int Id { get; set; }
    public ICollection<Gig> Positions { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }
    public List<Employer> Employers { get; set; }

    public Substitute()
    {
        Positions = new List<Gig>();
        Employers = new List<Employer>();
    }
}