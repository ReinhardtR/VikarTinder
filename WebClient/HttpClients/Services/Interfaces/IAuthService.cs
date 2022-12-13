using System.Security.Claims;

namespace HttpClients.Services.Interfaces;

public interface IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(string email, string password);
    public Task LogoutAsync();
    public Task SignUpEmployerAsync(SignUpEmployerRequestDTO dto);
    public Task SignUpSubstituteAsync(SignUpSubstituteRequestDTO dto);
    public Task GetUserInfoAsync(int userId);
    public Task<ClaimsPrincipal> GetAuthAsync();
}