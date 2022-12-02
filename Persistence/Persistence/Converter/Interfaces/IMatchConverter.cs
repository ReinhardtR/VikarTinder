using Persistence.Dto;
using Persistence.Models;

namespace Persistence.Converter.Interfaces;

public interface IMatchConverter
{

    MatchingSubstitutes ConvertSubList(List<Substitute> substitutes);
    MatchingGigs ConvertGigList(List<Gig> gigs);
    MatchValidation ConvertToValidation(IdsForMatchDto dto);
    ToBeMatchedDto CreateToBeMatchedDto(int requestCurrentUser, int requestToBeMatchedId, bool requestWantToMatch);
}