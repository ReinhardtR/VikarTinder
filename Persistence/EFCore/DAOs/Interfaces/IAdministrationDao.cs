using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Models;

namespace EFCore.DAOs.Interfaces;

public interface IAdministrationDao
{
    public Task<Substitute> CreateSubstituteAsync(string firstName, string lastName, string passwordHash, string email, Int32 age, string bio, string address);
    public Task<Employer> CreateEmployerAsync(string firstName, string lastName, string passwordHash, string email, string title, string workPlace);
    public Task<Substitute> UpdateSubstituteAsync(Substitute sub);
    public Task<Employer> UpdateEmployerAsync(Employer emp);
    public Task<UserDto> LoginAsync(string email, string password);
    public Task<DeleteUserDto> DeleteAccount(int id, DaoRequestType role);

    public Task<UserDto> GetUser(int id, DaoRequestType role);
}