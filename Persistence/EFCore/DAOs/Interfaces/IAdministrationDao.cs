using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Models;

namespace EFCore.DAOs.Interfaces;

public interface IAdministrationDao
{
    public Task<User> CreateUserAsync(string firstName, string lastName, string passwordHash, string email, Int32 age, string bio, string address);
    public Task<User> CreateUserAsync(string firstName, string lastName, string passwordHash, string email, string title, string workPlace);
    public Task<User> UpdateUserAsync(User user);
    public Task<User> LoginAsync(string email, string password);
    public Task<DeleteUserDto> DeleteAccount(int id, DaoRequestType role);
    public Task<User> GetUser(int id, DaoRequestType role);
}