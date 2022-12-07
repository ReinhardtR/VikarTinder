namespace Persistence.Models;

public class EmployerSubstitute
{
    public DateTime PublicationDate { get; set; }
    public bool WantsToMatch { get; set; }

    public int EmployerId { get; set; }
    public Employer Employer { get; set; }

    public int SubstituteId { get; set; }
    public Substitute Substitute { get; set; }
}