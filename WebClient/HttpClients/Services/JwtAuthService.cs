using System.Security.Claims;
using System.Text.Json;
using HttpClients.Services.Interfaces;

namespace HttpClients.Services;

public class JwtAuthService : IAuthService
{
    // cache the token
    public static string? Jwt { get; private set; } = "";
    
    private readonly IClient _client;
    
    public JwtAuthService(IClient client)
    {
        _client = client;
    }

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    
    public Task LoginAsync()
    {
        // call api to get token
        // Jwt = token;
        
        // ClaimsPrincipal principal = CreateClaimsPrincipal();
        //
        // OnAuthStateChanged.Invoke(principal);
        //
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    public Task RegisterAsync()
    {
        // call api
        throw new NotImplementedException();
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        return Task.FromResult(CreateClaimsPrincipal());
    }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
        
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
    
    // Below methods stolen from https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo/blob/master/demos/MissionControl/MissionControl.Client/Util/ServiceExtensions.cs
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}