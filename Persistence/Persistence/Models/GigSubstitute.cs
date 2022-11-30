namespace Persistence.Models;

public class GigSubstitute
{
    public DateTime PublicationDate { get; set; }

    public int SubstituteId { get; set; }
    public Substitute Substitute { get; set; }

    public int GigId { get; set; }
    public Gig Gig { get; set; }
}