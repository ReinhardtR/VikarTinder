using EFCore.DAOs.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Persistence;
using Persistence.DAOs;
using Persistence.Services;
using Persistence.Converter;
using Persistence.DAOs;
using Persistence.DAOs.Interfaces;
using Persistence.Services.Factories;
using AdministrationService = Persistence.Services.AdministrationService;
using MatchingService = Persistence.Services.MatchingService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5287, o => o.Protocols =
        HttpProtocols.Http2);
});

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<IChatDao, ChatDao>();
builder.Services.AddScoped<IJobConfirmationDao, JobConfirmationDao>();
builder.Services.AddScoped<IMatchDao, MatchDao>();
builder.Services.AddScoped<MatchFactory>();
builder.Services.AddScoped<MatchingService>();
builder.Services.AddScoped<AdministrationService>();
builder.Services.AddScoped<AdministrationFactory>();
builder.Services.AddScoped<IAdministrationDao, AdministrationDao>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ChatServiceServer>();
app.MapGrpcService<JobConfirmationServiceServer>();
app.MapGrpcService<MatchingService>();
app.MapGrpcService<AdministrationService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();