using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebClient.Utils;

public static class AuthClaims
{
    public const string Id = "ID";
    public const string FirstName = "NAMEF";
    public const string LastName = "NAMEL";
    public const string Email = "EMAIL";
    public const string Role = "ROLE";
    
    public static Claim GetClaim(AuthenticationState state, string claimName)
    {
        Claim? claim = state.User.Claims.FirstOrDefault((c) => c.Type.Equals(claimName));
        if (claim == null) throw new Exception($"Claim {claimName} not found");
        return claim;
    }
    
    public static int GetUserId(AuthenticationState state)
    {
        Claim claim = GetClaim(state, Id);
        return int.Parse(claim.Value);;
    }
    
    public static string GetEmail(AuthenticationState state)
    {
        Claim claim = GetClaim(state, Email);
        return claim.Value;
    }
    
    public static string GetRole(AuthenticationState state)
    {
        Claim claim = GetClaim(state, Role);
        return claim.Value;
    }
}