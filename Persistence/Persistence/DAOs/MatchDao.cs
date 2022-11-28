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
        
        _databaseContext.Add(new Substitute());
        _databaseContext.Add(new Gig(new Employer()));
    }
    public async Task<EmployerEFC> MatchWithEmployer(int currentUserId, int matchId)
    {
        //Finder sit eget user domæne objekt
        SubstituteEFC user = await GetSubstituteById(currentUserId);
        
        //Finder domæneobjeketet på ejeren af gigget man har matchet
        EmployerEFC employer = await GetEmployerById(matchId);
        
        
        //Adder den valgte gig sammen med tilhørende employer under sig
        user.Positions.Add(new Gig(employer));

        //Opdateringen og gemning
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.SubstituteEfcs.Update(user);
        await _databaseContext.SaveChangesAsync();

        return employer;
    }

    public async Task<SubstituteEFC> MatchWithSubstitute(int currentUserId, int matchId)
    {
        EmployerEFC employer = await GetEmployerById(matchId);

        SubstituteEFC substituteEfc = await GetSubstituteById(currentUserId);
        
        employer.Substitutes.Add(substituteEfc);
        
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.EmployerEfcs.Update(employer);
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

    public async Task<EmployerEFC> GetEmployerById(int id)
    {
        EmployerEFC? employer = await _databaseContext.EmployerEfcs.FirstOrDefaultAsync(emp =>
            emp.Id == id);
        
        return employer;
    }

    public async Task<SubstituteEFC> GetSubstituteById(int id)
    {
        SubstituteEFC? substitute = await _databaseContext.SubstituteEfcs.FirstOrDefaultAsync(sub =>
            sub.Id == id);
        return substitute;
    }
}