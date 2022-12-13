using Google.Protobuf.WellKnownTypes;
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
                User = CreateSubstituteUserInfo(substitute)
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
                User = CreateEmployerUserInfo(employer)
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
                response.User = CreateSubstituteUserInfo(substitute);
                return response;
            }
            else
            {
                Employer employer = (Employer)user;
                response.User = CreateEmployerUserInfo(employer);
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

        var birthdate = Timestamp.FromDateTime(substitute.BirthDate);
        
        return new UserObject
        {
            Id = substitute.Id,
            UserData = new UserData
            {
                Email = substitute.Email,
                FirstName = substitute.FirstName,
                LastName = substitute.LastName,
                PasswordHash = substitute.PasswordHash,
                Salt = substitute.Salt,
                Sub = new SubstituteObject
                {
                    Address = substitute.Address,
                    BirthDate = birthdate,
                    Bio = substitute.Bio
                }
            }
        };
    }

    private static UserInfo CreateSubstituteUserInfo(Substitute substitute)
    {
        var birthdate = Timestamp.FromDateTime(substitute.BirthDate);

        return new UserInfo
        {
            FirstName = substitute.FirstName,
            LastName = substitute.LastName,
            Sub = new SubstituteObject
            {
                Address = substitute.Address,
                BirthDate = birthdate,
                Bio = substitute.Bio
            }
        };
    }

    private static UserInfo CreateEmployerUserInfo(Employer employer)
    {
        return new UserInfo
        {
            FirstName = employer.FirstName,
            LastName = employer.LastName,
            Emp = new EmployerObject
            {
                Title = employer.Title,
                Workplace = employer.WorkPlace
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
                Salt = employer.Salt,
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
            if (updateUserRequest == null)
                throw new FactoryNullReference("UpdateUserRequest object or id");

            return new Substitute
            {
                FirstName = updateUserRequest.User.FirstName,
                LastName = updateUserRequest.User.LastName,
                BirthDate = updateUserRequest.User.Sub.BirthDate.ToDateTime(),
                Bio = updateUserRequest.User.Sub.Bio,
                Address = updateUserRequest.User.Sub.Address
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
            if (updateUserRequest == null)
                throw new FactoryNullReference("UpdateUserRequest object or id");

            return new Employer
            {
                FirstName = updateUserRequest.User.FirstName,
                LastName = updateUserRequest.User.LastName,
                Title = updateUserRequest.User.Emp.Title,
                WorkPlace = updateUserRequest.User.Emp.Workplace
            };
        }
        catch (Exception)
        {
            throw new FactoryNullReference("An UpdateUserRequest attribute");
        }
    }
}
