namespace Persistence.Models;

public class Gig
{
    public int Id { get; set; }
    public Employer Employer { get; set; }

    public Gig(Employer employer)
    {
        Employer = employer;
    }

    private Gig()
    {
    }
}