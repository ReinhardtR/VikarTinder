using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Chat
{
    [Key]
    public int Id { get; set; }

    public int SubstituteId { get; set; }

    public int EmployerId { get; set; } 
    
    public List<Message> Messages { get; set; }
    
    public JobConfirmation? JobConfirmation { get; set; }
    
}