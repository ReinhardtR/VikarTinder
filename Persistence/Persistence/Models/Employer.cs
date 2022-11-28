namespace Persistence.Models;

public class Employer
{
    public int Id { get; set; }
    public List<Substitute> Substitutes { get; set; }

    public Employer()
    {
    }
}