using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Chat
{
    [Key]
    public int Id { get; set; }

    public int SubstituteId { get; set; }
    public Substitute Substitute { get; set; }

    public int EmployerId { get; set; }
    public Employer Employer { get; set; }
    
    public int GigId { get; set; }
    public Gig Gig { get; set; }
    
    public List<Message> Messages { get; set; }
    
    public JobConfirmation? JobConfirmation { get; set; }
}