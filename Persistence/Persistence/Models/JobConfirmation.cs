using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class JobConfirmation : DateTrackingEntity
{
    [Key]
    public int id { get; set; }

    public Chat chat  { get; set; }
    
    public User substitute { get; set; }
    
    public User employer { get; set; }

    public bool isAccepted { get; set; }
    
}