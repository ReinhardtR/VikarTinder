using Persistence.Converter;
using Persistence.DAOs.Interfaces;
using Persistence.Dto;
using Persistence.Models;

namespace Persistence.Services;
using Grpc.Core;

public class MatchingService : Persistence.MatchingService.MatchingServiceBase
{
    private readonly IMatchDao _dao;
    
    public MatchingService(IMatchDao dao)
    {
        _dao = dao;
    }

    public override async Task<MatchValidationResponse> SendMatchFromSubstitute(MatchRequest request, ServerCallContext context)
    {
        Console.WriteLine("IDs: [Sub]:" + request.CurrentUser + " [Gig]:" + request.ToBeMatchedId);
        Console.WriteLine(request.WantToMatch);
        
        IdsForMatchDto matchedGig = await _dao.MatchingGig(
            MatchFactory.CreateToBeMatchedDto(
                request.CurrentUser,
                request.ToBeMatchedId,
                request.WantToMatch
            )
        );
        
        Console.WriteLine("Gig Id = " + matchedGig.GigId);
        
        MatchValidationResponse validation = MatchFactory.ConvertToValidation(matchedGig);
        Console.WriteLine("Conversion success");

        return validation;
    }
    
    public override async Task<MatchValidationResponse> SendMatchFromEmployer(MatchRequest request, ServerCallContext context)
    {
        IdsForMatchDto matchedSubstitute = await _dao.MatchingSubstitute(
            MatchFactory.CreateToBeMatchedDto(
                request.CurrentUser,
                request.ToBeMatchedId,
                request.WantToMatch
            )
        );
   
        MatchValidationResponse validation = MatchFactory.ConvertToValidation(matchedSubstitute);
        
        return validation;
    }

    public override async Task<SubstitutesForMatchingResponse> GetSubstitutes(SubstituteSearchParametersRequest request,
        ServerCallContext context)
    {
        // Resetter dem man har sagt nej til så de kan swipes igen
        await _dao.RemoveWhereTimerIsOut(request.CurrentUserId, DaoRequestType.Employer);
        
        // Databasekald for at få en liste af substitutes til matching
        List<Substitute> subsFromDatabase = await _dao.GetSubstitutesForMatching(request.CurrentUserId);

        // Convert til en reply
        SubstitutesForMatchingResponse subs =MatchFactory.ConvertSubList(subsFromDatabase);

        return subs;
    }

    public override async Task<GigsForMatchingResponse> GetGigs(GigSearchParametersRequest request, ServerCallContext context)
    {
        // Resetter dem man har sagt nej til så de kan swipes igen
        await _dao.RemoveWhereTimerIsOut(request.CurrentUserId,DaoRequestType.Substitute);
            
        // Databasekald for at få en liste af substitutes til matching
        List<Gig> gigsFromDatabase = await _dao.GetGigsForMatching(request.CurrentUserId);

        // Convert til en reply
        GigsForMatchingResponse gigs = MatchFactory.ConvertGigList(gigsFromDatabase);

        return gigs;
    }


}