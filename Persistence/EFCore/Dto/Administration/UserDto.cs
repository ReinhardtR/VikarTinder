using Persistence.Models;

namespace Persistence.Dto.Administration;

public class UserDto
{
    public Employer? Employer { get; set; }
    public Substitute? Substitute { get; set; }
    public DaoRequestType UserRole { get; set; }
}