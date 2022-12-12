using Persistence.Dto;
using Persistence.Dto.Auth;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace Persistence.Services.Factories;

public class AuthServiceFactory
{
    
    public static CreateUserResponse CreateSubstituteUserResponse(Substitute substitute)
    {
        if (substitute == null)
            throw new FactoryNullReference("substitute");

        try
        {
            CreateUserResponse response = new()
            {
                User = CreateSubstituteUserObject(substitute)
            };
            return response;
        }
        catch (Exception)
        {
            throw new FactoryNullReference("A substitute attribute");
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
        catch (Exception)
        {
            throw new FactoryNullReference("An employer attribute");
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
        catch (Exception)
        {
            throw new FactoryNullReference("A substitute attribute");
        }
        
    }
    
    public static UpdateUserResponse UpdateEmployerUserResponse(Employer employer)
    {
        if (employer == null)
        {
            throw new FactoryNullReference("employer");
        }

        try
        {
            UpdateUserResponse response = new UpdateUserResponse
            {
                User = CreateEmployerUserObject(employer)
            };

            return response;
        }
        catch (Exception)
        {
            throw new FactoryNullReference("An employer attribute");
        }
    }

    public static LoginUserResponse CreateLoginUserResponse(User user)
    {
        if (user == null)
            throw new FactoryNullReference("User");

        try
        {
            LoginUserResponse response = new();
            if (user is Substitute)
            {
                Substitute substitute = (Substitute)user;
                response.User = CreateSubstituteUserObject(substitute);
                return response;
            }
            else
            {
                Employer employer = (Employer)user;
                response.User = CreateEmployerUserObject(employer);
                return response;
            }
        }
        catch (Exception)
        {
            throw new FactoryNullReference("Values in User");
        }
    }
    

    public static GetUserResponse CreateGetUserResponse(User user)
    {
        if (user == null)
            throw new FactoryNullReference("User");

        try
        {
            GetUserResponse response = new();
            if (user is Substitute)
            {
                Substitute substitute = (Substitute)user;
                response.User = CreateSubstituteUserObject(substitute);
                return response;
            }
            else
            {
                Employer employer = (Employer)user;
                response.User = CreateEmployerUserObject(employer);
                return response;
            }
        }
        catch (Exception)
        {
            throw new FactoryNullReference("Values in User");
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
                FirstName = substitute.FirstName,
                LastName = substitute.LastName,
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
                FirstName = employer.FirstName,
                LastName = employer.LastName,
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
        if (dto == null || dto.Id == 0)
            throw new FactoryNullReference("DeleteUserDto or id");
        
        return new DeleteUserResponse
            {
                Validation = dto.Validation,
                User = new GetUserParams
                {
                    Id = dto.Id,
                    Role = dto.Role == DaoRequestType.Substitute
                        ? GetUserParams.Types.Role.Substitute
                        : GetUserParams.Types.Role.Employer
                }
            };
    }
    
    
    public static Substitute MakeSubstituteDomainObject(UpdateUserRequest updateUserRequest)
    {
        try
        {
            if (updateUserRequest == null || updateUserRequest.User.Id == 0)
                throw new FactoryNullReference("UpdateUserRequest object or id");

            return new Substitute
            {
                Id = updateUserRequest.User.Id,
                FirstName = updateUserRequest.User.UserData.FirstName,
                LastName = updateUserRequest.User.UserData.LastName,
                PasswordHash = updateUserRequest.User.UserData.PasswordHash,
                Email = updateUserRequest.User.UserData.Email,
                Age = updateUserRequest.User.UserData.Sub.Age,
                Bio = updateUserRequest.User.UserData.Sub.Bio,
                Address = updateUserRequest.User.UserData.Sub.Address
            };
        }
        catch (Exception)
        {
            throw new FactoryNullReference("An UpdateUserRequest attribute");
        }
    }

    public static Employer MakeEmployerDomainObject(UpdateUserRequest updateUserRequest)
    {

        try
        {
            if (updateUserRequest == null || updateUserRequest.User.Id == 0)
                throw new FactoryNullReference("UpdateUserRequest object or id");

            return new Employer
            {
                Id = updateUserRequest.User.Id,
                FirstName = updateUserRequest.User.UserData.FirstName,
                LastName = updateUserRequest.User.UserData.LastName,
                PasswordHash = updateUserRequest.User.UserData.PasswordHash,
                Email = updateUserRequest.User.UserData.Email,
                Title = updateUserRequest.User.UserData.Emp.Title,
                WorkPlace = updateUserRequest.User.UserData.Emp.Workplace
            };
        }
        catch (Exception)
        {
            throw new FactoryNullReference("An UpdateUserRequest attribute");
        }
    }
}
