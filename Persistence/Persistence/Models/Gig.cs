namespace Persistence.Models;

public class Gig
{
    public int Id { get; set; }
    public Employer Employer { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }
    public List<Substitute> Substitutes { get; set; }

    // TODO: why giving employer as parameter?
    //En gig kan ikke eksisterer uden employer
    public Gig(Employer employer)
    {
        Substitutes = new List<Substitute>();
        GigSubstitutes = new List<GigSubstitute>();
        Employer = employer;
    }

    public Gig()
    {
        Substitutes = new List<Substitute>();
        GigSubstitutes = new List<GigSubstitute>();
    }
}