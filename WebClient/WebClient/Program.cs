using HttpClients;
using HttpClients.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using WebClient;
using WebSockets;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<JobConfirmationService>();

builder.Services.AddScoped<Client>();
builder.Services.AddScoped<ChatSocket>();

builder.Services.AddMudServices((config) =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 100;
    config.SnackbarConfiguration.ShowTransitionDuration = 100;
});

await builder.Build().RunAsync();