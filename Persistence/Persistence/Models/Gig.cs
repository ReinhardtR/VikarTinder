namespace Persistence.Models;

public class Gig
{
    public int Id { get; set; }
    public Employer Employer { get; set; }
    
    public List<Substitute> Substitutes { get; set; }

    public Gig(Employer employer)
    {
        Substitutes = new List<Substitute>();
        Employer = employer;
    }

    private Gig()
    {
        Substitutes = new List<Substitute>();
    }
}