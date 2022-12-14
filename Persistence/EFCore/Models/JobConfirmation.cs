using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class JobConfirmation : DateTrackingEntity
{
    [Key]
    public int Id { get; set; }

    public int ChatId { get; set; }
    public Chat Chat  { get; set; }
    
    //Vi skal inkluderer en reference til en stilling, når vi merger. 
    
    public int SubstituteId { get; set; }
    public User Substitute { get; set; }
    
    public int EmployerId { get; set; }
    public User Employer { get; set; }
    
    public JobConfirmationStatus Status { get; set; }
    
    public bool IsTaken { get; set; } // if the job is taken by another substitute, bliver erstattet af stilling

    
}