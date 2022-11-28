namespace Persistence.Models;

public class EmployerEFC : Employer
{
    public List<Substitute> Substitutes { get; set; }

    public EmployerEFC()
    {
        
    }
}