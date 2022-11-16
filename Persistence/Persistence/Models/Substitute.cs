namespace Persistence.Models;

public class Substitute
{
    public int Id { get; private set; }
    public List<WorkPosition> Positions { get; set; }

    public Substitute()
    {
    }
}