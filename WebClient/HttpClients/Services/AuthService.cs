using System.Net.Http.Headers;
using System.Security.Claims;
using HttpClients.Services.Interfaces;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HttpClients.Services;

public class JwtAuthService : IAuthService
{
    // cache the token
    public static string? Jwt { get; private set; } = "";

    private readonly IGeneratedClient _client;
    
    public JwtAuthService(IGeneratedClient client)
    {
        _client = client;
    }

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    
    public async Task LoginAsync(string email, string password)
    {
        JwtResponseDTO jwtResponse = await _client.AuthenticateAsync(new LoginRequestDTO()
        {
            Email = email,
            Password = password
        });
        
        UpdateJwtToken(jwtResponse.JwtToken);
    }

    public Task LogoutAsync()
    {
        UpdateJwtToken(null);
        return Task.CompletedTask;
    }

    public Task SignUpEmployerAsync(SignUpEmployerRequestDTO dto)
    {
        return _client.RegisterEmployerAsync(dto);
    }
    
    public Task SignUpSubstituteAsync(SignUpSubstituteRequestDTO dto)
    {
        return _client.RegisterSubstituteAsync(dto);
    }

    public Task GetUserInfoAsync(int userId)
    {
        throw new NotImplementedException();
    }

    private void UpdateJwtToken(string? jwtToken)
    {
        Jwt = jwtToken;
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        OnAuthStateChanged.Invoke(principal);
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