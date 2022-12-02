namespace Persistence.Models;

public class Gig
{
    public int Id { get; set; }
    public Employer Employer { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }

    public ICollection<Substitute> Substitutes { get; set; }

    public Gig(Employer employer)
    {
        Substitutes = new List<Substitute>();
        GigSubstitutes = new List<GigSubstitute>();
        Employer = employer;
    }

    private Gig()
    {
        Substitutes = new List<Substitute>();
        GigSubstitutes = new List<GigSubstitute>();
    }
}