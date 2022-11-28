using Microsoft.EntityFrameworkCore;
using Persistence.DAOs.Interfaces;
using Persistence.Models;

namespace Persistence.DAOs;

public class MatchDao : IMatchDao
{
    private DatabaseContext _databaseContext;
    
    public MatchDao(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public async Task<Employer> MatchWithEmployer(int currentUserId, int matchId)
    {
        //Finder sit eget user domæne objekt
        Substitute user = await GetSubstituteById(currentUserId);
        
        //Finder domæneobjeketet på ejeren af gigget man har matchet
        Employer employer = await GetEmployerById(matchId);
        
        
        //Adder den valgte gig sammen med tilhørende employer under sig
        user.Positions.Add(new Gig(employer));

        //Opdateringen og gemning
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Substitutes.Update(user);
        await _databaseContext.SaveChangesAsync();

        return employer;
    }

    public async Task<Substitute> MatchWithSubstitute(int currentUserId, int matchId)
    {
        Employer employer = await GetEmployerById(matchId);

        Substitute substituteEfc = await GetSubstituteById(currentUserId);
        
        employer.Substitutes.Add(substituteEfc);
        
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Employers.Update(employer);
        await _databaseContext.SaveChangesAsync();

        return substituteEfc;
    }

    public async Task<List<Substitute>> GetSubstitutesForMatching(int userId)
    {
        //Få en list af substitutes
        
        //Listen af substitutes skal være en liste der ikke allerede er swipet nej til før
        //IMPLEMENTERES SENERE
        
        List<Substitute>? substitutes = new List<Substitute>();
        await _databaseContext.Substitutes.ForEachAsync(substitute =>
            substitutes.Add(substitute));

        return substitutes;
    }

    public async Task<List<Gig>> GetGigsForMatching(int id)
    {
        //Få en list af gigs
        
        //Gigs må ikke have været swipet nej til før
        //IKKE IMPLEMENTERET ENDNU
        
        List<Gig>? gigs = new List<Gig>();
        await _databaseContext.Gigs.ForEachAsync(gig =>
            gigs.Add(gig));

        return gigs;
    }

    public async Task<Employer> GetEmployerById(int id)
    {
        Employer? employer = await _databaseContext.Employers.FirstOrDefaultAsync(emp =>
            emp.Id == id);
        
        return employer;
    }

    public async Task<Substitute> GetSubstituteById(int id)
    {
        Substitute? substitute = await _databaseContext.Substitutes.FirstOrDefaultAsync(sub =>
            sub.Id == id);
        return substitute;
    }
}