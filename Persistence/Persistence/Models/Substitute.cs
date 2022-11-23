namespace Persistence.Models;

public class Substitute
{
    public int Id { get; set; }
    public List<Gig> Positions { get; set; }

    public Substitute()
    {
    }
}