using Persistence.DAOs.Interfaces;
using Persistence.Models;

namespace Persistence.DAOs;

public class MatchDao : IMatchDao
{
    public Task<Employer> MatchWithEmployer(int currentUserId, int matchId)
    {
        throw new NotImplementedException();
    }

    public Task<Substitute> MatchWithSubstitute(int currentUserId, int matchId)
    {
        throw new NotImplementedException();
    }

    public Task<Substitute> GetSubstituteById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Gig> GetWorkPositionBtId(int id)
    {
        throw new NotImplementedException();
    }
}