using System.Net.Http.Headers;
using HttpClients.Services;
using Newtonsoft.Json;

namespace HttpClients;

public partial class GeneratedClient
{
    partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
    {
        Console.WriteLine("UpdateJsonSerializerSettings");
        settings.NullValueHandling = NullValueHandling.Ignore;
    }
    
    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        string? token = JwtAuthService.Jwt;
        request.Headers.Authorization = token == null
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }
}