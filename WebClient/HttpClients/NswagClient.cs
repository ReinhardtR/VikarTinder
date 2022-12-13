using System.Net.Http.Headers;
using HttpClients.Services;
using Newtonsoft.Json;

namespace HttpClients;

public partial class GeneratedClient
{
    partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
    {
        settings.NullValueHandling = NullValueHandling.Ignore;
        settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    }
    
    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        string? token = AuthService.Jwt;
        request.Headers.Authorization = token == null
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }
}