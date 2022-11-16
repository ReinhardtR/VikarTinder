namespace Persistence.Models;

public class Employer
{
    public int Id { get; private set; }
    public List<Substitute> Substitutes { get; set; }

    private Employer()
    {
    }
}