using EFCore.DAOs.Interfaces;
using Grpc.Core;
using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Models;
using Persistence.Services.Factories;

namespace Persistence.Services;

public class AdministrationService : Persistence.AdministrationService.AdministrationServiceBase
{
    private IAdministrationDao _dao;

    public AdministrationService(IAdministrationDao dao)
    {
        _dao = dao;
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest,
        ServerCallContext context)
    {
        //TODO: Skal eventuelt også være en dto
        //TODO: eller bare et if statement lul
        dynamic user = createUserRequest.User.RoleCase == UserData.RoleOneofCase.Sub
            ? await _dao.CreateSubstituteAsync(
                createUserRequest.User.Name,
                createUserRequest.User.PasswordHash,
                createUserRequest.User.Email,
                createUserRequest.User.Sub.Age,
                createUserRequest.User.Sub.Bio,
                createUserRequest.User.Sub.Address)
            : await _dao.CreateEmployerAsync(
                createUserRequest.User.Name,
                createUserRequest.User.PasswordHash,
                createUserRequest.User.Email,
                createUserRequest.User.Emp.Title,
                createUserRequest.User.Emp.Workplace);

        CreateUserResponse userResponse = createUserRequest.User.RoleCase == UserData.RoleOneofCase.Sub
            ? AdministrationFactory.CreateSubstiuteUserResponse((Substitute)user)
            : AdministrationFactory.CreateEmployerUserResponse((Employer)user);

        return userResponse;
    }

    public override async Task<LoginUserResponse> Login(CreateLoginRequest createLoginRequest,
        ServerCallContext serverCallContext)
    {
        LoginUserDto user = await _dao.LoginAsync(createLoginRequest.Email, createLoginRequest.PasswordHash);

        LoginUserResponse userResponse = user.UserRole == DaoRequestType.Substitute
            ? AdministrationFactory.LoginSubstituteUserResponse(user.Substitute)
            : AdministrationFactory.LoginEmployerUserResponse(user.Employer);

        return userResponse;
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest updateUserRequest,
        ServerCallContext serverCallContext)
    {
        //TODO: se ovenstående todo
        dynamic user = updateUserRequest.User.UserData.RoleCase == UserData.RoleOneofCase.Sub
            ? await _dao.UpdateSubstituteAsync(AdministrationFactory.MakeSubstituteDomainObject(updateUserRequest))
            : await _dao.UpdateEmployerAsync(AdministrationFactory.MakeEmployerDomainObject(updateUserRequest));

        UpdateUserResponse userResponse = updateUserRequest.User.UserData.RoleCase == UserData.RoleOneofCase.Sub
            ? AdministrationFactory.UpdateSubstituteUserResponse((Substitute)user)
            : AdministrationFactory.UpdateEmployerUserResponse((Employer)user);

        return userResponse;
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest,
        ServerCallContext serverCallContext)
    {
        DaoRequestType userRole = deleteUserRequest.User.Role == UserToDeleteParams.Types.Role.Substitute
            ? DaoRequestType.Substitute
            : DaoRequestType.Employer;
        
        DeleteUserDto deletedUser = _dao.DeleteAccount(deleteUserRequest.User.Id, userRole);

        DeleteUserResponse deletedUserResponse = AdministrationFactory.CreateDeleteUserResponse(deletedUser);

        return deletedUserResponse;
    }
}