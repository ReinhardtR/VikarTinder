using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class JobConfirmation : DateTrackingEntity
{
    [Key]
    public int id { get; set; }

    public Chat chat  { get; set; }
    
    //Vi skal inkluderer en reference til en stilling, når vi merger. 
    
    public User substitute { get; set; }
    
    public User employer { get; set; }

    public bool isAccepted { get; set; }
    
    public bool isTaken { get; set; } // if the job is taken by another substitute, bliver erstattet af stilling
    
    
}