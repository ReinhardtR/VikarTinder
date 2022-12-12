namespace Persistence.Dto.Auth;

public class DeleteUserDto
{
    public int Id { get; set; }
    public DaoRequestType Role { get; set; }
    public bool Validation { get; set; }
}