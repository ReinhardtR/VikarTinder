using Persistence.Models;

namespace Persistence.DAOs.Interfaces;

public interface IMatchDao
{
    Task<Employer> MatchWithEmployer(int currentUserId, int matchId);
    Task<Substitute> MatchWithSubstitute(int currentUserId, int matchId);


    Task<List<Substitute>> GetSubstituteById(int id);
    Task<List<Gig>> GetGigById(int id);
}