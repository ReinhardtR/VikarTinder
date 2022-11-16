using Grpc.Core;
using Persistence;

namespace Persistence.Services;

public class GreeterService : UserService.UserServiceBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        Console.WriteLine("ID: " + request.Id);
        return Task.FromResult(new HelloReply
        {
            
            Message = "Hello " + request.Id
        });
    }
}