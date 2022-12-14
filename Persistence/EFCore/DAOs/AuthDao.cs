using EFCore.DAOs.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Dto;
using Persistence.Dto.Auth;
using Persistence.Exceptions.DaoExceptions;
using Persistence.Models;

namespace Persistence.DAOs;

public class AuthDao : IAuthDao
{
    private readonly DataContext _dataContext;
    
    public AuthDao(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<User> CreateUserAsync(string firstName, string lastName, string passwordHash,string salt, string email, DateTime birthDate, string bio,
        string address)
    {
        try
        {
            var substitute = await _dataContext.Substitutes.AddAsync(new Substitute
            {
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = passwordHash,
                Salt = salt,
                Email = email,
                Address = address,
                BirthDate = birthDate,
                Bio = bio
            });
            await _dataContext.SaveChangesAsync();

            //Tjek om den substitute der kommer tilbage har fået et id nu :)
            Console.WriteLine(substitute.Entity.Id);
        
            return substitute.Entity;
        }
        catch (Exception)
        {
            throw new DaoNotUniqueEmail("Email is already in use");
        }
    }

    public async Task<User> CreateUserAsync(string firstName, string lastName, string passwordHash, string salt, string email,
        string title, string workPlace)
    {
        try
        {
            var employer = await _dataContext.Employers.AddAsync(new Employer
            {
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = passwordHash,
                Salt = salt,
                Email = email,
                Title = title,
                WorkPlace = workPlace
            });

            await _dataContext.SaveChangesAsync();

            //Tjek om den employer der kommer tilbage har fået et id nu :)
            Console.WriteLine(employer.Entity.Id);

            return employer.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new DaoNotUniqueEmail("Email is already in use");
        }
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        Console.WriteLine("User id: " + user.Id);
        User? userToUpdate = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        
        if (userToUpdate == null)
        {
            throw new DaoNullReference("User not found");
        }

        if (userToUpdate is Substitute)
        {
            Substitute newSubstituteData = (Substitute)user;
            Substitute substituteToUpdate = (Substitute)userToUpdate;

            substituteToUpdate.FirstName = newSubstituteData.FirstName;
            substituteToUpdate.LastName = newSubstituteData.LastName;

            substituteToUpdate.Address = newSubstituteData.Address;
            substituteToUpdate.BirthDate = newSubstituteData.BirthDate;
            substituteToUpdate.Bio = newSubstituteData.Bio;
            
            _dataContext.Substitutes.Update(substituteToUpdate);
            await _dataContext.SaveChangesAsync();
            return substituteToUpdate;
        }
        
        if (userToUpdate is Employer)
        {
            Employer newEmployerData = (Employer)user;
            Employer employerToUpdate = (Employer)userToUpdate;

            employerToUpdate.FirstName = newEmployerData.FirstName;
            employerToUpdate.LastName = newEmployerData.LastName;

            employerToUpdate.Title = newEmployerData.Title;
            employerToUpdate.WorkPlace = newEmployerData.WorkPlace;
            
            _dataContext.Employers.Update(employerToUpdate);
            await _dataContext.SaveChangesAsync();
            return employerToUpdate;
        }

        return null;
    }

    public async Task<User> LoginAsync(string email)
    {
        //Nok nemmere at teste fra client hehe leles
        User? user = await _dataContext.Users.FirstOrDefaultAsync(u =>
            u.Email.Equals(email));
        
        if (user == null)
            throw new DaoNullReference("User not found cant login");

        return user;
    }

    public async Task<DeleteUserDto> DeleteAccount(int id, DaoRequestType role)
    {
        //Tjek om brugeren er korrekt fjernet fra databasen
        DeleteUserDto deleteUserDto = new();
        
        if (role == DaoRequestType.Substitute)
        {
            Substitute? user = await _dataContext.Substitutes.FirstOrDefaultAsync(substitute => substitute.Id == id);
            if (user == null)
                throw new DaoNullReference("Substitute not found");
            
            _dataContext.Substitutes.Remove(user);
            await _dataContext.SaveChangesAsync();

            deleteUserDto.Id = user.Id;
            deleteUserDto.Role = DaoRequestType.Substitute;
            deleteUserDto.Validation = true;
        }
        else
        {
            Employer? user = await _dataContext.Employers.FirstOrDefaultAsync(employer => employer.Id == id);
            if (user == null)
                throw new DaoNullReference("Employer not found");
            
            _dataContext.Employers.Remove(user);
            await _dataContext.SaveChangesAsync();
            
            deleteUserDto.Id = user.Id;
            deleteUserDto.Role = DaoRequestType.Employer;
            deleteUserDto.Validation = true;
        }

        return deleteUserDto;
    }

    public async Task<User> GetUser(int id, DaoRequestType role)
    {
        User? user = role == DaoRequestType.Substitute
            ? await _dataContext.Substitutes.FirstOrDefaultAsync(substitute => substitute.Id == id)
            : await _dataContext.Employers.FirstOrDefaultAsync(employer => employer.Id == id);
        
        if (user == null) throw new DaoNullReference("User not found");
        
        return user;
    }
}