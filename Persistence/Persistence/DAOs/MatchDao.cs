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
        Console.WriteLine("SUB: " + substitute.Id);
        
        //Finder domæneobjeketet på ejeren af gigget man har matchet
        Gig gig = await GetGigById(matchId);
        Console.WriteLine("GIG: " + gig.Id);
        
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
        
        
        employer.Substitutes.Add(substitute);
        
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Employers.Update(employer);
        
        await _databaseContext.SaveChangesAsync();

        return substitute;
    }

    public async Task<List<Substitute>> GetSubstitutesForMatching(int userId)
    {
        //Få en list af substitutes
        
        //Listen af substitutes skal være en liste der ikke allerede er swipet ja til før
        IQueryable<Substitute> subsQuery = _databaseContext.Substitutes.AsQueryable();

        subsQuery = subsQuery.Where(substitute =>
            substitute.Employers.Any() == false ||
            substitute.Employers.All(employer => employer.Id != userId) == true);

        return await subsQuery.ToListAsync();
    }

    public async Task<List<Gig>> GetGigsForMatching(int id)
    {
        //Få en list af gigs
        
        //Gigs må ikke have været swipet ja til før

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
        Substitute? substitute = await _databaseContext.Substitutes.FirstOrDefaultAsync(sub => sub.Id == id);

        return substitute;

    }

    public async Task<Gig> GetGigById(int id)
    {

        Gig? gigToReturn = await _databaseContext.Gigs.FirstOrDefaultAsync(gig => gig.Id == id);

        return gigToReturn;
    }

    public async Task<bool> CheckIfMatchedEmpSub(int empId, int subId)
    {
        IQueryable<Employer> employersQ = _databaseContext.Employers.Include(employer => employer.Substitutes);
        Employer? emp = await employersQ.FirstOrDefaultAsync(employer => employer.Id == empId);

        if (emp.Substitutes.All(substitute => substitute.Id != subId))
        {
            return true;
        }
        return false;
    }

    public async Task<bool> CheckIfMatchedSubGig(int subId, int gigId)
    {
        IQueryable<Substitute> substituteQ = _databaseContext.Substitutes.Include(substitute => substitute.Positions);
        Substitute? sub = await substituteQ.FirstOrDefaultAsync(substitute => substitute.Id == subId);

        if (sub.Positions.All(gig => gig.Id != gigId))
        {
            return true;
        }
        return false;
    }
}