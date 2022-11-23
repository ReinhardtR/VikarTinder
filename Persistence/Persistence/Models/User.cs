using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public List<Chat> Chats { get; set; }
}