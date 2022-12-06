namespace Persistence.Models;

public class Gig
{
    public int Id { get; set; }
    public Employer Employer { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }

    // TODO: why use icollection here and list above?
    public ICollection<Substitute> Substitutes { get; set; }

    // TODO: why giving employer as parameter?
    public Gig(Employer employer)
    {
        Substitutes = new List<Substitute>();
        GigSubstitutes = new List<GigSubstitute>();
        Employer = employer;
    }

    // TODO: not used
    private Gig()
    {
        Substitutes = new List<Substitute>();
        GigSubstitutes = new List<GigSubstitute>();
    }
}