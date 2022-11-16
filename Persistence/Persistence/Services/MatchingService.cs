using Persistence.Logic.Interfaces;

namespace Persistence.Services;
using Grpc.Core;

public class MatchingService : Persistence.MatchingService.MatchingServiceBase
{
    private readonly IMatchAdapter Adapter;

    public MatchingService(IMatchAdapter adapter)
    {
        Adapter = adapter;
    }
    
    public override Task<Validation> MatchForSub(SendSubAndWorkp request, ServerCallContext context)
    {
        Console.WriteLine("IDs: [Client]:" + request.Substitute.Id + " [WorkP]:" + request.Workp.Id);
        
        Adapter.MatchForSub(request);

        return Task.FromResult(new Validation
        {
            Confirmation = true
        });
    }
}