using Persistence.Models;

namespace Persistence.Converter.Interfaces;

public interface IMatchConverter
{
    MatchValidation EmployerConverter(EmployerEFC employer, int userId);
    MatchValidation SubstituteConverter(SubstituteEFC substitute, int userId);

    MatchingSubstitutes ConvertSubList(List<Substitute> substitutes);
    MatchingGigs ConvertGigList(List<Gig> gigs);
}