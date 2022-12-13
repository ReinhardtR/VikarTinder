using System.Security.Claims;
using HttpClients.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebClient.Providers;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public CustomAuthProvider(AuthService authService)
    {
        _authService = authService;
        _authService.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await _authService.GetAuthAsync();
        
        Console.WriteLine("IsAuthorized: " + principal.Identity?.IsAuthenticated);
        Console.WriteLine("Name: " + principal.Identity?.Name);
        
        foreach (Claim principalClaim in principal.Claims)
        {
            Console.WriteLine($"Name: {principalClaim.Type}, Value: {principalClaim.Value}");
        }

        return new AuthenticationState(principal);
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}