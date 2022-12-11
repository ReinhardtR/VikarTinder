using System.Collections.Immutable;
using Persistence.Dto;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace Persistence.Converter;

public class MatchFactory
{
    public static SubstitutesForMatchingResponse ConvertSubList(List<Substitute> substitutes)
    {
        if (substitutes == null)
            throw new FactoryNullReference("Substitute list");
        
        SubstitutesForMatchingResponse subs = new();

        if (substitutes.Count == 0) return subs;
        
        foreach (Substitute sub in substitutes)
        {
            if (sub.Id == 0)
                throw new FactoryNullReference("Substitute id");
            
            subs.Substitutes.Add(new SubstituteToBeMatched
            {
                Id = sub.Id
            });
        }

        return subs;
    }
    
    public static GigsForMatchingResponse ConvertGigList(List<Gig> gigs)
    {
        if (gigs == null)
            throw new FactoryNullReference("Gigs list");
        
        GigsForMatchingResponse gigsGrpc = new();

        if (gigs.Count != 0)
        {
            foreach (var gig in gigs)
            {
                if (gig.Id == 0)
                    throw new FactoryNullReference("Gig id");
                
                gigsGrpc.Gigs.Add(new GigToBeMatched
                {
                    Id = gig.Id
                });
            }
        }

        return gigsGrpc;
    }

    public static MatchValidationResponse ConvertToValidation(IdsForMatchDto dto)
    {
        if (dto == null || dto.EmployerId == 0 || dto.SubstituteId == 0)
            throw new FactoryNullReference("IdsForMatch argument ");
        
        MatchValidationResponse val = new()
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