using Microsoft.AspNetCore.Server.Kestrel.Core;
using Persistence;
using Persistence.Converter;
using Persistence.DAOs;
using Persistence.DAOs.Interfaces;
using MatchingService = Persistence.Services.MatchingService;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<IMatchDao, MatchDao>();
builder.Services.AddScoped<MatchConverter>();
builder.Services.AddScoped<MatchingService>();
builder.Services.AddScoped<DatabaseContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<MatchingService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();