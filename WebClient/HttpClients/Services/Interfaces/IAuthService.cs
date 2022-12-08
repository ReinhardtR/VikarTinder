using System.Security.Claims;

namespace HttpClients.Services.Interfaces;

public interface IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync();
    public Task LogoutAsync();
    public Task RegisterAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
}