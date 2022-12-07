using System.Collections.Immutable;
using Persistence.Dto;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace Persistence.Converter;

public class MatchFactory
{
    public static MatchingSubstitutes ConvertSubList(List<Substitute> substitutes)
    {
        if (substitutes == null)
            throw new FactoryNullReference("Substitute list");
        
        MatchingSubstitutes subs = new();

        if (substitutes.Count == 0) return subs;
        
        foreach (Substitute sub in substitutes)
        {
            subs.Substitutes.Add(new SubstituteToBeMatched
            {
                Id = sub.Id
            });
        }

        return subs;
    }
    
    public static MatchingGigs ConvertGigList(List<Gig> gigs)
    {
        if (gigs == null)
            throw new FactoryNullReference("Gigs list");
        
        MatchingGigs gigsGrpc = new();

        if (gigs.Count != 0)
        {
            foreach (var gig in gigs)
            {
                gigsGrpc.Gigs.Add(new GigToBeMatched
                {
                    Id = gig.Id
                });
            }
        }

        return gigsGrpc;
    }

    public static MatchValidation ConvertToValidation(IdsForMatchDto dto)
    {
        if (dto == null)
            throw new FactoryNullReference("IdsForMatch argument ");
        
        MatchValidation val = new()
            {
                EmployerId = dto.EmployerId,
                GigId = dto.GigId,
                IsMatched = dto.WasAMatch,
                SubstituteId = dto.SubstituteId
            };

        return val;
    }

    public static ToBeMatchedDto CreateToBeMatchedDto(int requestCurrentUser, int requestToBeMatchedId, bool requestWantToMatch)
    {
        ToBeMatchedDto dto = new()
        {
            UserId = requestCurrentUser,
            MatchId = requestToBeMatchedId,
            WantsToMatch = requestWantToMatch
        };
        
        return dto;
    }
}