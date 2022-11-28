using Persistence.Models;

namespace Persistence.DAOs.Interfaces;

public interface IMatchDao
{
    Task<EmployerEFC> MatchWithEmployer(int currentUserId, int matchId);
    Task<SubstituteEFC> MatchWithSubstitute(int currentUserId, int matchId);


    Task<List<Substitute>> GetSubstitutesForMatching(int id);
    Task<List<Gig>> GetGigsForMatching(int id);

    Task<EmployerEFC> GetEmployerById(int id);
    Task<SubstituteEFC> GetSubstituteById(int id);


}