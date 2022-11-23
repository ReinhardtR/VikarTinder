using Persistence.Converter.Interfaces;
using Persistence.Models;

namespace Persistence.Converter;

public class MatchConverter : IMatchConverter
{
    public MatchValidation EmployerConverter(Employer employer, int userId)
    {
        MatchValidation val = new MatchValidation
        {
            MatchId = employer.Id,
            IsMatched = false
        };
        
        foreach (Substitute subs in employer.Substitutes)
        {
            if (subs.Id == userId)
            {
                val.IsMatched = true;
            }
        }

        return val;
    }

    public MatchValidation SubstituteConverter(Substitute substitute, int userId)
    {
        throw new NotImplementedException();
    }
}