using Persistence.Converter.Interfaces;
using Persistence.Dto;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace Persistence.Converter;

public class MatchConverter : IMatchConverter
{
    
    
    public MatchingSubstitutes ConvertSubList(List<Substitute> substitutes)
    {
        if (substitutes == null || substitutes.Count.Equals(0))
            throw new ConverterNullReference("Substitute list");
        
        MatchingSubstitutes subs = new MatchingSubstitutes();

        foreach (var sub in substitutes)
        {
            subs.Substitutes.Add(new SubstituteToBeMatched
            {
                Id = sub.Id
            });
        }

        return subs;
    }
    
    public MatchingGigs ConvertGigList(List<Gig> gigs)
    {
        MatchingGigs gigsGrpc = new MatchingGigs();

        foreach (var gig in gigs)
        {
            gigsGrpc.Gigs.Add(new GigToBeMatched
            {
                Id = gig.Id
            });
        }

        return gigsGrpc;
    }

    public MatchValidation ConvertToValidation(IdsForMatchDto dto)
    {
        MatchValidation val = new MatchValidation
        {
            EmployerId = dto.EmployerId,
            GigId = dto.GigId,
            IsMatched = dto.WasAMatch,
            SubstituteId = dto.SustituteId
        };

        return val;
    }

    public ToBeMatchedDto CreateToBeMatchedDto(int requestCurrentUser, int requestToBeMatchedId, bool requestWantToMatch)
    {
        ToBeMatchedDto dto = new ToBeMatchedDto()
        {
            UserId = requestCurrentUser,
            MatchId = requestToBeMatchedId,
            WantsToMatch = requestWantToMatch
        };
        return dto;
    }
}