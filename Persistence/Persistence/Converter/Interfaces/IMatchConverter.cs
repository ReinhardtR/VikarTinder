using Persistence.Models;

namespace Persistence.Converter.Interfaces;

public interface IMatchConverter
{
    MatchValidation GigConverter(Gig gig, int userId);
    MatchValidation SubstituteConverter(Substitute substitute, int userId);

    MatchingSubstitutes ConvertSubList(List<Substitute> substitutes);
    MatchingGigs ConvertGigList(List<Gig> gigs);
}