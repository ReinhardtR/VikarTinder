using Google.Protobuf.WellKnownTypes;
using Persistence.Dto;
using Persistence.Dto.Auth;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace Persistence.Services.Factories;

public class AuthServiceFactory
{
    
    public static CreateUserResponse CreateUserResponse(User user)
    {
        if (user == null)
            throw new FactoryNullReference("User");

        try
        {
            CreateUserResponse response = new();
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

    public static UpdateUserResponse CreateUpdateUserResponse(User user)
    {
        if (user == null)
            throw new FactoryNullReference("User");

        try
        {
            UpdateUserResponse response = new();
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
        if (substitute == null)
            throw new FactoryNullReference("substitute");
        
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
        if (employer == null)
            throw new FactoryNullReference("Employer");
        
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
    
    //Id == 0 should throw since it means getting an empty Id from gRPC client - Same logic to underlying methods
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
            if (updateUserRequest == null ||updateUserRequest.Id == 0) 
                throw new FactoryNullReference("UpdateUserRequest object or Id");

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
            if (updateUserRequest == null || updateUserRequest.Id == 0)
                throw new FactoryNullReference("UpdateUserRequest object or Id");

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
