using EFCore.DAOs.Interfaces;
using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Models;

namespace Persistence.DAOs;

public class AdministrationDao : IAdministrationDao
{
    public Task<Substitute> CreateSubstituteAsync(string name, string passwordHash, string email, int age, string bio, string address)
    {
        throw new NotImplementedException();
    }

    public Task<Employer> CreateEmployerAsync(string name, string passwordHash, string email, string title, string workPlace)
    {
        throw new NotImplementedException();
    }

    public Task<Substitute> UpdateSubstituteAsync(Substitute sub)
    {
        throw new NotImplementedException();
    }

    public Task<Employer> UpdateEmployerAsync(Employer emp)
    {
        throw new NotImplementedException();
    }

    public Task<LoginUserDto> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public DeleteUserDto DeleteAccount(int id, DaoRequestType role)
    {
        throw new NotImplementedException();
    }
}