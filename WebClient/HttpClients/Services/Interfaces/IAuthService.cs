using System.Security.Claims;

namespace HttpClients.Services.Interfaces;

public interface IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(string email, string password);
    public Task LogoutAsync();
    public Task RegisterEmployerAsync(RegisterEmployerRequestDTO dto);
    public Task RegisterSubstituteAsync(RegisterSubstituteRequestDTO dto);
    public Task<ClaimsPrincipal> GetAuthAsync();
}