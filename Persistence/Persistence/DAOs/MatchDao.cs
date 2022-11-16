using Persistence.DAOs.Interfaces;
using Persistence.Models;

namespace Persistence.DAOs;

public class MatchDao : IMatchDao
{
    public Action MakeMatch(Substitute sub, WorkPosition workp)
    {
        throw new NotImplementedException();
    }

    public Task<Substitute> GetSubstituteById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<WorkPosition> GetWorkPositionBtId(int id)
    {
        throw new NotImplementedException();
    }
}