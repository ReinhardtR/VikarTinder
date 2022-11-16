namespace Persistence.Models;

public class WorkPosition
{
    public int Id { get; private set; }
    public Employer Employer { get; set; }

    public WorkPosition(Employer employer)
    {
        Employer = employer;
    }

    private WorkPosition()
    {
    }
}