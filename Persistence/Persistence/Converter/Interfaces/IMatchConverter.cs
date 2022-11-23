using Persistence.Models;

namespace Persistence.Converter.Interfaces;

public interface IMatchConverter
{
    MatchValidation EmployerConverter(Employer employer, int userId);
    MatchValidation SubstituteConverter(Substitute substitute, int userId);
}