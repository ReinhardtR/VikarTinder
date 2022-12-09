using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace Persistence.Services.Factories;

public class AdministrationFactory
{
    public static CreateUserResponse CreateSubstiuteUserResponse(Substitute substitute)
    {
        if (substitute == null)
            throw new FactoryNullReference("substitute");

        try
        {
            CreateUserResponse response = new CreateUserResponse
            {
                User = CreateSubstituteUserObject(substitute)
            };
            return response;
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("A substitute attribute " + e.Message);
        }
        
    }

    public static CreateUserResponse CreateEmployerUserResponse(Employer employer)
    {
        if (employer == null)
            throw new FactoryNullReference("employer");

        try
        {
            CreateUserResponse response = new CreateUserResponse
            {
                User = CreateEmployerUserObject(employer)
            };
            return response;
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("An employer attribute " + e.Message);
        }
    }

    public static UpdateUserResponse UpdateSubstituteUserResponse(Substitute substitute)
    {
        if (substitute == null)
            throw new FactoryNullReference("substitute");

        try
        {
            UpdateUserResponse response = new UpdateUserResponse
            {
                User = CreateSubstituteUserObject(substitute)
            };

            return response;

        }
        catch (Exception e)
        {
            throw new FactoryNullReference("A substitute attribute " + e.Message);
        }
        
    }
    
    public static UpdateUserResponse UpdateEmployerUserResponse(Employer employer)
    {
        if (employer == null)
            throw new FactoryNullReference("employer");


        try
        {

            UpdateUserResponse response = new UpdateUserResponse
            {
                User = CreateEmployerUserObject(employer)
            };

            return response;
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("An employer attribute " + e.Message);
        }
    }

    public static LoginUserResponse LoginSubstituteUserResponse(Substitute substitute)
    {
        if (substitute == null)
            throw new FactoryNullReference("substitute");

        try
        {
            LoginUserResponse response = new LoginUserResponse
            {
                User = CreateSubstituteUserObject(substitute)
            };
            return response;
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("A substitute attribute " + e.Message);
        }
    }
    
    public static LoginUserResponse LoginEmployerUserResponse(Employer employer)
    {
        if (employer == null)
            throw new FactoryNullReference("employer");

        try
        {

            LoginUserResponse response = new LoginUserResponse
            {
                User = CreateEmployerUserObject(employer)
            };
            return response;
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("An employer attribute " + e.Message);
        }
    }

    private static UserObject CreateSubstituteUserObject(Substitute substitute)
    {
        if (substitute == null)
            throw new FactoryNullReference("substitute");
        
        return new UserObject
        {
            Id = substitute.Id,
            UserData = new UserData
            {
                Email = substitute.Email,
                Name = substitute.Name,
                PasswordHash = substitute.PasswordHash,
                Sub = new SubstituteObject
                {
                    Address = substitute.Address,
                    Age = substitute.Age,
                    Bio = substitute.Bio
                }
            }
        };
    }

    private static UserObject CreateEmployerUserObject(Employer employer)
    {
        if (employer == null)
            throw new FactoryNullReference("employer");
        
        return new UserObject
        {
            Id = employer.Id,
            UserData = new UserData
            {
                Email = employer.Email,
                Name = employer.Name,
                PasswordHash = employer.PasswordHash,
                Emp = new EmployerObject
                {
                    Title = employer.Title,
                    Workplace = employer.WorkPlace
                }
            }
        };
    }

    public static DeleteUserResponse CreateDeleteUserResponse(DeleteUserDto dto)
    {
        if (dto == null)
            throw new FactoryNullReference("DeleteUserDto");

        try
        {

            return new DeleteUserResponse
            {
                Validation = dto.validation,
                User = new UserToDeleteParams
                {
                    Id = dto.id,
                    Role = dto.role == DaoRequestType.Substitute
                        ? UserToDeleteParams.Types.Role.Substitute
                        : UserToDeleteParams.Types.Role.Employer
                }
            };
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("A DeleteUserDto attribute " + e.Message);
        }
    }

    public static Substitute MakeSubstituteDomainObject(UpdateUserRequest updateUserRequest)
    {
        if (updateUserRequest == null)
            throw new FactoryNullReference("UpdateUserRequest");

        try
        {

            return new Substitute
            {
                Id = updateUserRequest.User.Id,
                Name = updateUserRequest.User.UserData.Name,
                PasswordHash = updateUserRequest.User.UserData.PasswordHash,
                Email = updateUserRequest.User.UserData.Email,
                Age = updateUserRequest.User.UserData.Sub.Age,
                Bio = updateUserRequest.User.UserData.Sub.Bio,
                Address = updateUserRequest.User.UserData.Sub.Address
            };
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("An UpdateUserRequest attribute " + e.Message);
        }
    }

    public static Employer MakeEmployerDomainObject(UpdateUserRequest updateUserRequest)
    {
        if (updateUserRequest == null)
            throw new FactoryNullReference("UpdateUserRequest");

        try
        {

            return new Employer
            {
                Id = updateUserRequest.User.Id,
                Name = updateUserRequest.User.UserData.Name,
                PasswordHash = updateUserRequest.User.UserData.PasswordHash,
                Email = updateUserRequest.User.UserData.Email,
                Title = updateUserRequest.User.UserData.Emp.Title,
                WorkPlace = updateUserRequest.User.UserData.Emp.Workplace
            };
        }
        catch (Exception e)
        {
            throw new FactoryNullReference("An UpdateUserRequest attribute " + e.Message);
        }
    }
}
