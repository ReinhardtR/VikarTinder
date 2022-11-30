using Persistence.Models;

namespace Persistence.DAOs.Interfaces;

public interface IMatchDao
{
    Task<Gig> MatchWithGig(int currentUserId, int matchId);
    Task<Substitute> MatchWithSubstitute(int currentUserId, int matchId);


    Task<List<Substitute>> GetSubstitutesForMatching(int id);
    Task<List<Gig>> GetGigsForMatching(int id);
    
    
    Task<Employer> GetEmployerById(int id);
    Task<Substitute> GetSubstituteById(int id);
    Task<Gig> GetGigById(int id);

    Task<bool> CheckIfMatchedEmpSub(int empId, int subId);
    Task<bool> CheckIfMatchedSubGig(int subId, int gigId);

}