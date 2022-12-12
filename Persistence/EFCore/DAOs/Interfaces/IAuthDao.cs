using Persistence.Dto;
using Persistence.Dto.Auth;
using Persistence.Models;

namespace EFCore.DAOs.Interfaces;

public interface IAuthDao
{
    public Task<User> CreateUserAsync(string firstName, string lastName, string passwordHash, string email, DateTime birthDate, string bio, string address);
    public Task<User> CreateUserAsync(string firstName, string lastName, string passwordHash, string email, string title, string workPlace);
    public Task<User> UpdateUserAsync(User user);
    public Task<User> LoginAsync(string email);
    public Task<DeleteUserDto> DeleteAccount(int id, DaoRequestType role);
    public Task<User> GetUser(int id, DaoRequestType role);
}