using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebClient.Utils;

public static class AuthClaims
{
    public const string Id = "ID";
    public const string Name = "NAME";
    public const string Email = "EMAIL";
    public const string Role = "ROLE";
    
    public static Claim? GetClaim(AuthenticationState state, string claim)
    {
        return state.User.Claims.FirstOrDefault((c) => c.Type.Equals(claim));
    }
}