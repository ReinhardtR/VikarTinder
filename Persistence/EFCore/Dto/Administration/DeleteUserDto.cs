namespace Persistence.Dto.Administration;

public class DeleteUserDto
{
    public int Id { get; set; }
    public DaoRequestType Role { get; set; }
    public bool Validation { get; set; }
}