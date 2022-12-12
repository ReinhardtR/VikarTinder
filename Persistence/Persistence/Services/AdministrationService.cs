using EFCore.DAOs.Interfaces;
using Grpc.Core;
using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Exceptions.DaoExceptions;
using Persistence.Models;
using Persistence.Services.Factories;

namespace Persistence.Services;

public class AdministrationService : Persistence.AdministrationService.AdministrationServiceBase
{
    private readonly IAdministrationDao _dao;

    public AdministrationService(IAdministrationDao dao)
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
                    createUserRequest.User.Email,
                    createUserRequest.User.Sub.Age,
                    createUserRequest.User.Sub.Bio,
                    createUserRequest.User.Sub.Address)
                : await _dao.CreateUserAsync(
                    createUserRequest.User.FirstName,
                    createUserRequest.User.LastName,
                    createUserRequest.User.PasswordHash,
                    createUserRequest.User.Email,
                    createUserRequest.User.Emp.Title,
                    createUserRequest.User.Emp.Workplace);
            
            CreateUserResponse userResponse = user is Substitute
                ? AdministrationFactory.CreateSubstiuteUserResponse((Substitute)user)
                : AdministrationFactory.CreateEmployerUserResponse((Employer)user);

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
            LoginUserResponse userResponse = AdministrationFactory.CreateLoginUserResponse(user);

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
        User user = updateUserRequest.User.UserData.RoleCase == UserData.RoleOneofCase.Sub
            ? await _dao.UpdateUserAsync(AdministrationFactory.MakeSubstituteDomainObject(updateUserRequest))
            : await _dao.UpdateUserAsync(AdministrationFactory.MakeEmployerDomainObject(updateUserRequest));

        UpdateUserResponse userResponse = user is Substitute
            ? AdministrationFactory.UpdateSubstituteUserResponse((Substitute)user)
            : AdministrationFactory.UpdateEmployerUserResponse((Employer)user);

        return userResponse;
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest,
        ServerCallContext serverCallContext)
    {
        DaoRequestType userRole = deleteUserRequest.User.Role == GetUserParams.Types.Role.Substitute
            ? DaoRequestType.Substitute
            : DaoRequestType.Employer;
        
        DeleteUserDto deletedUser = await _dao.DeleteAccount(deleteUserRequest.User.Id, userRole);

        DeleteUserResponse deletedUserResponse = AdministrationFactory.CreateDeleteUserResponse(deletedUser);

        return deletedUserResponse;
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest getUserRequest,
        ServerCallContext serverCallContext)
    {
        DaoRequestType role = getUserRequest.User.Role == GetUserParams.Types.Role.Substitute
            ? DaoRequestType.Substitute
            : DaoRequestType.Employer;
        User user = await _dao.GetUser(getUserRequest.User.Id, role);

        GetUserResponse userResponse = AdministrationFactory.CreateGetUserResponse(user);

        return userResponse;
    }
}