using Persistence.Models;

namespace Persistence.DAOs.Interfaces;

public interface IMatchDao
{
    Action MakeMatch(Substitute sub, WorkPosition workp);
    Task<Substitute> GetSubstituteById(int id);
    Task<WorkPosition> GetWorkPositionBtId(int id);
}