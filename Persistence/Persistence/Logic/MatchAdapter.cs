using Microsoft.AspNetCore.Mvc;
using Persistence.DAOs.Interfaces;
using Persistence.Logic.Interfaces;
using Persistence.Models;

namespace Persistence.Logic;

public class MatchAdapter : IMatchAdapter
{
    private readonly IMatchDao _matchDao;

    public MatchAdapter(IMatchDao matchDao)
    {
        _matchDao = matchDao;
    }
    public async Task<Task> MatchForSub(SendSubAndWorkp request)
    {
        _matchDao.MakeMatch(
            await CreateSubstitute(request.Substitute),
            await CreateWorkplace(request.Workp));

        return Task.CompletedTask;
    }

    private async Task<WorkPosition> CreateWorkplace(WorkpId requestWorkp)
    {
        return await _matchDao.GetWorkPositionBtId(requestWorkp.Id);
    }


    private async Task<Substitute> CreateSubstitute(SubstituteId requestSubstitute)
    {
        return await _matchDao.GetSubstituteById(requestSubstitute.Id);
    }
}