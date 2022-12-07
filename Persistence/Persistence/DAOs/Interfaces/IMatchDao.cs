using Persistence.Dto;
using Persistence.Models;

namespace Persistence.DAOs.Interfaces;

public interface IMatchDao
{
    Task<IdsForMatchDto> MatchingGig(ToBeMatchedDto dto);
    Task<IdsForMatchDto> MatchingSubstitute(ToBeMatchedDto dto);
    Task<List<Substitute>> GetSubstitutesForMatching(int id);
    Task<List<Gig>> GetGigsForMatching(int id);
    Task<IdsForMatchDto> CheckIfMatched(IdsForMatchDto dto);

    Task RemoveWhereTimerIsOut(int id, DaoRequestType type);
}