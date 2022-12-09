using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebClient.Utils;

public static class AuthClaims
{
    public const string Id = "id";
    public const string Name = "name";
    public const string Email = "email";
    public const string Role = "role";
    
    public static Claim? GetClaim(AuthenticationState state, string claim)
    {
        return state.User.Claims.FirstOrDefault((c) => c.Type.Equals(claim));
    }
}