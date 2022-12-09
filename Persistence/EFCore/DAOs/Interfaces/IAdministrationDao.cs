using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Models;

namespace EFCore.DAOs.Interfaces;

public interface IAdministrationDao
{
    public Task<Substitute> CreateSubstituteAsync(string name, string passwordHash, string email, Int32 age, string bio, string address);
    public Task<Employer> CreateEmployerAsync(string name, string passwordHash, string email, string title, string workPlace);
    public Task<Substitute> UpdateSubstituteAsync(Substitute sub);
    public Task<Employer> UpdateEmployerAsync(Employer emp);
    public Task<LoginUserDto> LoginAsync(string email, string password);
    public DeleteUserDto DeleteAccount(int id, DaoRequestType role);
}