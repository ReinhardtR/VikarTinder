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
    public async Task<Gig> MatchWithGig(int currentUserId, int matchId)
    {
        //Finder sit eget user domæne objekt
        Substitute substitute = await GetSubstituteById(currentUserId);
        
        //Finder domæneobjeketet på ejeren af gigget man har matchet
        Gig gig = await GetGigById(matchId);
        
        
        //Adder den valgte gig sammen med tilhørende employer under sig
        substitute.Positions.Add(gig);

        //Opdateringen og gemning
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();

        return gig;
    }

    public async Task<Substitute> MatchWithSubstitute(int currentUserId, int matchId)
    {
        Employer employer = await GetEmployerById(currentUserId);
        Console.WriteLine(employer.Id + " EMPLOYER");

        Substitute substitute = await GetSubstituteById(matchId);
        Console.WriteLine(substitute.Id + " SUB");
        
        employer.Substitutes.Add(substitute);
        
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Employers.Update(employer);
        await _databaseContext.SaveChangesAsync();

        return substitute;
    }

    public async Task<List<Substitute>> GetSubstitutesForMatching(int userId)
    {
        //Få en list af substitutes
        
        //Listen af substitutes skal være en liste der ikke allerede er swipet nej til før
        //IMPLEMENTERES SENERE
        IQueryable<Substitute> subsQuery = _databaseContext.Substitutes.AsQueryable();

        subsQuery = subsQuery.Where(substitute =>
            substitute.Employers.Any() == false ||
            substitute.Employers.All(employer => employer.Id != userId) == true);

        return await subsQuery.ToListAsync();
    }

    public async Task<List<Gig>> GetGigsForMatching(int id)
    {
        //Få en list af gigs
        
        //Gigs må ikke have været swipet nej til før
        //IKKE IMPLEMENTERET ENDNU
        

        //Hope it works :3
        IQueryable<Gig> gigsQuery = _databaseContext.Gigs.AsQueryable();
        
        gigsQuery = gigsQuery.Where(gig => 
            gig.Substitutes.Any() == false ||
            gig.Substitutes.All(substitute => substitute.Id != id) == true);
        
            return await gigsQuery.ToListAsync();
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

    public async Task<Gig> GetGigById(int id)
    {
        Gig? gigs = await _databaseContext.Gigs.FirstOrDefaultAsync(gig => gig.Id == id);
        
        return gigs;
    }
}