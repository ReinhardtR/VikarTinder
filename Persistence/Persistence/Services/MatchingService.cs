using Persistence.Converter.Interfaces;
using Persistence.DAOs.Interfaces;
using Persistence.Models;

namespace Persistence.Services;
using Grpc.Core;

public class MatchingService : Persistence.MatchingService.MatchingServiceBase
{

    private readonly IMatchConverter _converter;
    private readonly IMatchDao _dao;
    public MatchingService(IMatchConverter converter, IMatchDao dao)
    {
        _converter = converter;
        _dao = dao;
    }


    public override async Task<MatchValidation> SendMatchFromSubstitute(MatchRequest request, ServerCallContext context)
    {
        Console.WriteLine("IDs: [Sub]:" + request.CurrentUser + " [Gig]:" + request.ToBeMatchedId);
        
        //DatabaseKald for at matche
        Employer matchedEmployer = await _dao.MatchWithEmployer(request.CurrentUser, request.ToBeMatchedId);

        //Convert tilbage til reqly
        MatchValidation val = _converter.EmployerConverter(matchedEmployer, request.CurrentUser);

        return val;
    }

}