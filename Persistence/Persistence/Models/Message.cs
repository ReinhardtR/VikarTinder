using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

public class Message
{
    [Key]
    public int Id { get; set; }
    
    public User Author { get; set; }
    public int AuthorId { get; set; }

    public Chat Chat { get; set; }
    public int ChatId { get; set; }
    
    public string Content { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime SentAt { get; set; }
    
    
}