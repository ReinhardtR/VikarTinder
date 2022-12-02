using Persistence.Converter.Interfaces;
using Persistence.DAOs.Interfaces;
using Persistence.Dto;
using Persistence.Models;

namespace Persistence.Services;
using Grpc.Core;

public class MatchingService : Persistence.MatchingService.MatchingServiceBase
{
    /*
     insert into Gigs values (1, 1), (2,1), (3,1), (4, 1), (5,1);
insert into Employers values (1);
insert into Substitutes values (1);  
     */

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
        Console.WriteLine(request.WantToMatch);
        //DatabaseKald for at matche
        IdsForMatchDto matchedGig = await _dao.MatchingGig(
            _converter.CreateToBeMatchedDto(
                request.CurrentUser,
                request.ToBeMatchedId,
                request.WantToMatch));
        
        Console.WriteLine("Gig Id = " + matchedGig.GigId);
        
        //Finding if matched
        if (request.WantToMatch)
        {
            matchedGig = await _dao.CheckIfMatched(matchedGig);
        }

        //Convert tilbage til reqly
        MatchValidation val = _converter.ConvertToValidation(matchedGig);
        Console.WriteLine("Conversion success");

        return val;
    }
    
    public override async Task<MatchValidation> SendMatchFromEmployer(MatchRequest request, ServerCallContext context)
    {
        Console.WriteLine("IDs: [Employer]:" + request.CurrentUser + " [Substitute]:" + request.ToBeMatchedId);
        
        
        
        //DatabaseKald for at matche
        IdsForMatchDto matchedSubstitute = await _dao.MatchingSubstitute(_converter.CreateToBeMatchedDto(
            request.CurrentUser,
            request.ToBeMatchedId,
            request.WantToMatch));
        
        //Setting match
        if (request.WantToMatch)
        {
            matchedSubstitute = await _dao.CheckIfMatched(matchedSubstitute);
        }

        //Conversion
        MatchValidation val = _converter.ConvertToValidation(matchedSubstitute);
        
        Console.WriteLine("Conversion success: [SUB ID]"+ val.SubstituteId + "[ISMATCHED]"+val.IsMatched);
        return val;
    }

    public override async Task<MatchingSubstitutes> GetSubstitutes(SubstituteSearchParameters request,
        ServerCallContext context)
    {
        //Resetter dem man har sagt nej til så de kan swipes igen
        await _dao.RemoveWhereTimerIsOut(request.CurrentUserId, DaoRequestType.Employer);
        
        //Databasekald for at få en liste af substitutes til matching
        List<Substitute> subsFromDatabase = await _dao.GetSubstitutesForMatching(request.CurrentUserId);


        //Convert til en reply
        MatchingSubstitutes subs =_converter.ConvertSubList(subsFromDatabase);

        return subs;
    }

    public override async Task<MatchingGigs> GetGigs(GigSearchParameters request, ServerCallContext context)
    {
        //Resetter dem man har sagt nej til så de kan swipes igen
        await _dao.RemoveWhereTimerIsOut(request.CurrentUserId,DaoRequestType.Substitute);
            
        //Databasekald for at få en liste af substitutes til matching
        List<Gig> gigsFromDatabase = await _dao.GetGigsForMatching(request.CurrentUserId);

        //Convert til en reply
        MatchingGigs  gigs = _converter.ConvertGigList(gigsFromDatabase);

        return gigs;
    }


}