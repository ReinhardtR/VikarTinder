namespace Persistence.Dto.Administration;

public class DeleteUserDto
{
    public int id { get; set; }
    public DaoRequestType role { get; set; }
    public bool validation { get; set; }
}