using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class Chat
{
    [Key]
    public int Id { get; set; }

    public List<User> Participants { get; set; }
    
    public List<Message> Messages { get; set; }
    
    
}