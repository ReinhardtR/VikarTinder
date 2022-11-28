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
        Console.WriteLine("Employer Id = " + matchedEmployer.Id);
        
        //Convert tilbage til reqly
        MatchValidation val = _converter.EmployerConverter(matchedEmployer, request.CurrentUser);

        return val;
    }
    
    public override async Task<MatchValidation> SendMatchFromEmployer(MatchRequest request, ServerCallContext context)
    {
        Console.WriteLine("IDs: [Employer]:" + request.CurrentUser + " [Substitute]:" + request.ToBeMatchedId);
        
        //DatabaseKald for at matche
        Substitute matchedSubstitute = await _dao.MatchWithSubstitute(request.CurrentUser, request.ToBeMatchedId);

        //Convert tilbage til reqly
        MatchValidation val = _converter.SubstituteConverter(matchedSubstitute, request.CurrentUser);

        return val;
    }

    public override async Task<MatchingSubstitutes> GetSubstitutes(SubstituteSearchParameters request,
        ServerCallContext context)
    {
        //Databasekald for at få en liste af substitutes til matching
        List<Substitute> subsFromDatabase = await _dao.GetSubstitutesForMatching(request.CurrentUserId);


        //Convert til en reply
        MatchingSubstitutes subs =_converter.ConvertSubList(subsFromDatabase);

        return subs;
    }

    public override async Task<MatchingGigs> GetGigs(GigSearchParameters request, ServerCallContext context)
    {
        //Databasekald for at få en liste af substitutes til matching
        List<Gig> gigsFromDatabase = await _dao.GetGigsForMatching(request.CurrentUserId);

        //Convert til en reply
        MatchingGigs  gigs = _converter.ConvertGigList(gigsFromDatabase);

        return gigs;
    }


}