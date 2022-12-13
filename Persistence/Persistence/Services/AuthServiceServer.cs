using EFCore.DAOs.Interfaces;
using Grpc.Core;
using Persistence.Dto;
using Persistence.Dto.Auth;
using Persistence.Exceptions.DaoExceptions;
using Persistence.Models;
using Persistence.Services.Factories;

namespace Persistence.Services;

public class AuthServiceServer : AuthService.AuthServiceBase
{
    private readonly IAuthDao _dao;

    public AuthServiceServer(IAuthDao dao)
    {
        _dao = dao;
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest,
        ServerCallContext context)
    {
        try
        {
            User user = createUserRequest.User.RoleCase == UserData.RoleOneofCase.Sub
                ? await _dao.CreateUserAsync(
                    createUserRequest.User.FirstName,
                    createUserRequest.User.LastName,
                    createUserRequest.User.PasswordHash,
                    createUserRequest.User.Salt,
                    createUserRequest.User.Email,
                    createUserRequest.User.Sub.BirthDate.ToDateTime(),
                    createUserRequest.User.Sub.Bio,
                    createUserRequest.User.Sub.Address)
                : await _dao.CreateUserAsync(
                    createUserRequest.User.FirstName,
                    createUserRequest.User.LastName,
                    createUserRequest.User.PasswordHash,
                    createUserRequest.User.Salt,
                    createUserRequest.User.Email,
                    createUserRequest.User.Emp.Title,
                    createUserRequest.User.Emp.Workplace);

            CreateUserResponse userResponse = AuthServiceFactory.CreateUserResponse(user);
            return userResponse;
        }
        catch (DaoNotUniqueEmail)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Email is already in use"));
        }
        
    }

    public override async Task<LoginUserResponse> Login(CreateLoginRequest createLoginRequest,
        ServerCallContext serverCallContext)
    {
        try
        {
            User user = await _dao.LoginAsync(createLoginRequest.Email);
            LoginUserResponse userResponse = AuthServiceFactory.CreateLoginUserResponse(user);

            return userResponse;
        }
        catch (DaoNullReference)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Access denied"));
        }
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest updateUserRequest,
        ServerCallContext serverCallContext)
    {
        User user = updateUserRequest.User.RoleCase == UserInfo.RoleOneofCase.Sub
            ? await _dao.UpdateUserAsync(AuthServiceFactory.MakeSubstituteDomainObject(updateUserRequest))
            : await _dao.UpdateUserAsync(AuthServiceFactory.MakeEmployerDomainObject(updateUserRequest));

        UpdateUserResponse userResponse = AuthServiceFactory.CreateUpdateUserResponse(user);

        return userResponse;
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest,
        ServerCallContext serverCallContext)
    {
        DaoRequestType userRole = deleteUserRequest.User.Role == GetUserParams.Types.Role.Substitute
            ? DaoRequestType.Substitute
            : DaoRequestType.Employer;
        
        DeleteUserDto deletedUser = await _dao.DeleteAccount(deleteUserRequest.User.Id, userRole);

        DeleteUserResponse deletedUserResponse = AuthServiceFactory.CreateDeleteUserResponse(deletedUser);

        return deletedUserResponse;
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest getUserRequest,
        ServerCallContext serverCallContext)
    {
        DaoRequestType role = getUserRequest.User.Role == GetUserParams.Types.Role.Substitute
            ? DaoRequestType.Substitute
            : DaoRequestType.Employer;
        User user = await _dao.GetUser(getUserRequest.User.Id, role);

        GetUserResponse userResponse = AuthServiceFactory.CreateGetUserResponse(user);

        return userResponse;
    }
}