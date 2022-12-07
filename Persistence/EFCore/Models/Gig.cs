namespace Persistence.Models;

public class Gig
{
    public int Id { get; set; }
    public Employer Employer { get; set; }
    public List<GigSubstitute> GigSubstitutes { get; set; }
    public List<Substitute> Substitutes { get; set; }
}