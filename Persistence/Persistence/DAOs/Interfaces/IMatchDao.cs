using Persistence.Models;

namespace Persistence.DAOs.Interfaces;

public interface IMatchDao
{
    Task<Employer> MatchWithEmployer(int currentUserId, int matchId);
    Task<Substitute> MatchWithSubstitute(int currentUserId, int matchId);


    Task<Substitute> GetSubstituteById(int id);
    Task<Gig> GetWorkPositionBtId(int id);
}