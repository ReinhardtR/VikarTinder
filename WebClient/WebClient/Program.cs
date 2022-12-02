using HttpClients;
using HttpClients.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebClient;
using WebSockets;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<Client>();
builder.Services.AddScoped<ChatSocket>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();