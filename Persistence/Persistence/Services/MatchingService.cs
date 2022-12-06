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

    public override async Task<MatchValidation> SendMatchFromSubstitute(MatchRequest request, ServerCallContext context)
    {
        Console.WriteLine("IDs: [Sub]:" + request.CurrentUser + " [Gig]:" + request.ToBeMatchedId);
        Console.WriteLine(request.WantToMatch);
        
        //DatabaseKald for at matche
        IdsForMatchDto matchedGig = await _dao.MatchingGig(
            MatchConverter.CreateToBeMatchedDto(
                request.CurrentUser,
                request.ToBeMatchedId,
                request.WantToMatch));
        
        Console.WriteLine("Gig Id = " + matchedGig.GigId);

        //Convert tilbage til reqly
        MatchValidation validation = MatchConverter.ConvertToValidation(matchedGig);
        Console.WriteLine("Conversion success");

        return validation;
    }
    
    public override async Task<MatchValidation> SendMatchFromEmployer(MatchRequest request, ServerCallContext context)
    {
        Console.WriteLine("IDs: [Employer]:" + request.CurrentUser + " [Substitute]:" + request.ToBeMatchedId);
        
        //DatabaseKald for at matche
        IdsForMatchDto matchedSubstitute = await _dao.MatchingSubstitute(MatchConverter.CreateToBeMatchedDto(
            request.CurrentUser,
            request.ToBeMatchedId,
            request.WantToMatch));
   
        //Conversion
        MatchValidation validation = MatchConverter.ConvertToValidation(matchedSubstitute);
        
        Console.WriteLine("Conversion success: [SUB ID]"+ validation.SubstituteId + "[ISMATCHED]" + validation.IsMatched);
        return validation;
    }

    public override async Task<MatchingSubstitutes> GetSubstitutes(SubstituteSearchParameters request,
        ServerCallContext context)
    {
        // Resetter dem man har sagt nej til så de kan swipes igen
        await _dao.RemoveWhereTimerIsOut(request.CurrentUserId, DaoRequestType.Employer);
        
        // Databasekald for at få en liste af substitutes til matching
        List<Substitute> subsFromDatabase = await _dao.GetSubstitutesForMatching(request.CurrentUserId);

        // Convert til en reply
        MatchingSubstitutes subs =MatchConverter.ConvertSubList(subsFromDatabase);

        return subs;
    }

    public override async Task<MatchingGigs> GetGigs(GigSearchParameters request, ServerCallContext context)
    {
        //Resetter dem man har sagt nej til så de kan swipes igen
        await _dao.RemoveWhereTimerIsOut(request.CurrentUserId,DaoRequestType.Substitute);
            
        //Databasekald for at få en liste af substitutes til matching
        List<Gig> gigsFromDatabase = await _dao.GetGigsForMatching(request.CurrentUserId);

        //Convert til en reply
        MatchingGigs gigs = MatchConverter.ConvertGigList(gigsFromDatabase);

        return gigs;
    }


}