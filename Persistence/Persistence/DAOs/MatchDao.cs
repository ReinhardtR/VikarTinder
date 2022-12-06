using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.DAOs.Interfaces;
using Persistence.Dto;
using Persistence.Models;

namespace Persistence.DAOs;

public class MatchDao : IMatchDao
{
    private readonly DatabaseContext _databaseContext;

    public MatchDao(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IdsForMatchDto> MatchingGig(ToBeMatchedDto dto)
    {
        if (dto.WantsToMatch)
        {
            return await MatchWithGig(dto);
        }
  
        return await DontWantGig(dto);
    }

    // TODO: merge "match gig" and "dont want to match gig" into one method
    private async Task<IdsForMatchDto> DontWantGig(ToBeMatchedDto dto)
    {
        Substitute substitute = await GetSubstituteById(dto.UserId);
        Gig gig = await GetGigById(dto.MatchId);
        
        IdsForMatchDto idsForMatch = new()
        {
            SustituteId = substitute.Id,
            GigId = gig.Id,
            EmployerId = gig.Employer.Id
        };

        //Adder den valgte gig under sig
        substitute.GigSubstitutes.Add(new GigSubstitute
        {
            Gig = gig,
            Substitute = substitute,
            WantsToMatch = false
        });
        
        //Opdateringen og gemning
        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();
        
        // TODO: what dis do?
        _databaseContext.ChangeTracker.Clear();

        return idsForMatch;
    }

    private async Task<IdsForMatchDto> MatchWithGig(ToBeMatchedDto dto)
    {
        Substitute substitute = await GetSubstituteById(dto.UserId);
        Console.WriteLine("SUB: " + substitute.Id);
        
        Gig gig = await GetGigById(dto.MatchId);
        Console.WriteLine("GIG: " + gig.Id);
        
        IdsForMatchDto idsForMatch = new()
        {
            SustituteId = substitute.Id,
            GigId = gig.Id,
            EmployerId = gig.Employer.Id
        };

        //Adder den valgte gig
        substitute.GigSubstitutes.Add(new GigSubstitute
        {
            Gig = gig,
            Substitute = substitute,
            WantsToMatch = true
        });
        
        //Opdateringen og gemning
        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();
        
        // TODO: what dis do?
        _databaseContext.ChangeTracker.Clear();

        return idsForMatch;
    }
    
    // TODO: merge "match" and "dont match" into one method
    public async Task<IdsForMatchDto> MatchingSubstitute(ToBeMatchedDto dto)
    {
        if (dto.WantsToMatch)
        {
            return await MatchWithSubstitute(dto);
        }
     
        return await DontWantSubstitute(dto);
    }

    private async Task<IdsForMatchDto> DontWantSubstitute(ToBeMatchedDto dto)
    {
        Employer employer = await GetEmployerById(dto.UserId);
        Console.WriteLine(employer.Id + " EMPLOYER");

        Substitute substitute = await GetSubstituteById(dto.MatchId);
        
        employer.EmployerSubstitutes.Add(new EmployerSubstitute
        {
            Employer = employer,
            Substitute = substitute,
            WantsToMatch = false
        });
        
        employer.Substitutes.Add(substitute);

        IdsForMatchDto idsForMatch = new()
        {
            SustituteId = substitute.Id,
            EmployerId = employer.Id
        };

        _databaseContext.Employers.Update(employer);
        await _databaseContext.SaveChangesAsync();
        
        // TODO: what dis do?
        _databaseContext.ChangeTracker.Clear();
        
        return idsForMatch;
    }

    private async Task<IdsForMatchDto> MatchWithSubstitute(ToBeMatchedDto dto)
    {
        Employer employer = await GetEmployerById(dto.UserId);
        Console.WriteLine(employer.Id + " EMPLOYER");
        
        Substitute substitute = await GetSubstituteById(dto.MatchId);
        
        employer.EmployerSubstitutes.Add(new EmployerSubstitute
        {
            Employer = employer,
            Substitute = substitute,
            WantsToMatch = true
        });

        IdsForMatchDto idsForMatch = new()
        {
            SustituteId = substitute.Id,
            EmployerId = employer.Id
        };
        
        _databaseContext.Employers.Update(employer);
        await _databaseContext.SaveChangesAsync();
        
        // TODO: what dis do?
        _databaseContext.ChangeTracker.Clear();

        return idsForMatch;
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

    // TODO: private much? null return type?
    public async Task<Gig> GetGigById(int id)
    {
        Gig? gigToReturn = await _databaseContext.Gigs.Include(gig => gig.Employer)
            .FirstOrDefaultAsync(gig => gig.Id == id);

        return gigToReturn;
    }

    public async Task<IdsForMatchDto> CheckIfMatched(IdsForMatchDto dto)
    {
        CheckMatchDto check = await CheckMatchQuery(dto);
        
        dto.WasAMatch = check.WasAMatch;
        dto.GigId = check.GigId;
        
        return dto;
    }

    // TODO: investigate filtering by date in the query,
    // TODO: instead of removing old answers before using the query.
    public async Task RemoveWhereTimerIsOut(int id, DaoRequestType type)
    {
        DateTime currentDateTime = DateTime.UtcNow;
        TimeSpan span= new(1,0,0);

        switch (type)
        {
            case DaoRequestType.Employer:
                RemoveWhereTimerIsOutEmpSub(id, currentDateTime, span);
                break;
            case DaoRequestType.Substitute:
                RemoveWhereTimerIsOutSubGig(id, currentDateTime, span);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private async void RemoveWhereTimerIsOutSubGig(int id, DateTime currentDateTime, TimeSpan span)
    {

        var substitute = await _databaseContext.Substitutes.Include(sub =>
            sub.EmployerSubstitutes).FirstOrDefaultAsync(sub => sub.Id == id);

        foreach (var gigSub in substitute.GigSubstitutes)
        {
            Console.WriteLine("REAL TIME:"+ currentDateTime + "GIG SUB" +gigSub.PublicationDate +" " + gigSub.WantsToMatch);
        }

        var amountRemoved = substitute.GigSubstitutes.RemoveAll(gigsub =>
            gigsub.WantsToMatch == false && currentDateTime - gigsub.PublicationDate > span);
        Console.WriteLine(amountRemoved);
        Console.WriteLine(substitute.GigSubstitutes.Count);

        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();
        
        // todo what dis do?
        _databaseContext.ChangeTracker.Clear();
    }

    private async void RemoveWhereTimerIsOutEmpSub(int id, DateTime currentDateTime, TimeSpan span)
    {
        var employer = await _databaseContext.Employers.Include(emp =>
            emp.EmployerSubstitutes).FirstOrDefaultAsync(emp => emp.Id == id);
        

        foreach (var empSub in employer.EmployerSubstitutes)
        {
            Console.WriteLine("REAL TIME:"+ currentDateTime + "EMP SUB" +empSub.PublicationDate +" " + empSub.WantsToMatch);
        }

        var amountRemoved = employer.EmployerSubstitutes.RemoveAll(empSub =>
            empSub.WantsToMatch == false && currentDateTime - empSub.PublicationDate > span);
        Console.WriteLine(amountRemoved);
        Console.WriteLine(employer.EmployerSubstitutes.Count);

        _databaseContext.Employers.Update(employer);
        await _databaseContext.SaveChangesAsync();
        _databaseContext.ChangeTracker.Clear();
    }


    private async Task<CheckMatchDto> CheckMatchQuery(IdsForMatchDto dto)
    {
        //Check om employer-substitute har et element med hhv. subid og empid
        //Der på samtidig er true for begge

        var query = from gigSubs in _databaseContext.Set<GigSubstitute>()
            join empSubs in _databaseContext.Set<EmployerSubstitute>()
                on new { id = gigSubs.Substitute, dto.SustituteId }
                equals new { id = empSubs.Substitute, dto.SustituteId }
            join gigs in _databaseContext.Set<Gig>().Include(gig => gig.Employer)
                on new { id = gigSubs.GigId }
                equals new { id = gigs.Id }
            select new { empSubs, gigSubs, gigs };

        foreach (var var in query)
        {
            Console.WriteLine("GIGS" + var.gigs.Id +" " + var.gigs.Employer.Id +" "
            + " EMPSUBS" + var.empSubs.EmployerId +" " + var.empSubs.WantsToMatch +" "
            + "GIGSUBS" + var.gigSubs.SubstituteId + " "+ var.gigSubs.WantsToMatch);
        }
        
        // TODO: first condition is sus? change to &&
        var result = query.FirstOrDefaultAsync(joined =>
            joined.gigSubs.WantsToMatch == joined.empSubs.WantsToMatch // <- this
            && joined.empSubs.EmployerId == joined.gigs.Employer.Id
            && joined.empSubs.EmployerId == dto.EmployerId
            && joined.gigSubs.SubstituteId == dto.SustituteId
        ).Result;

        if (result != null)
        {
            Console.WriteLine("RESULT" + result.gigs.Id + " " + result.empSubs.EmployerId + " " + result.gigSubs.GigId);
            Console.WriteLine("DTO: " + dto.GigId + " "+ dto.EmployerId + " " + dto.SustituteId);
            return new CheckMatchDto
            {
                GigId = result.gigs.Id,
                WasAMatch = true
            };
        }
      
        Console.WriteLine("NO MATCH");
        return new CheckMatchDto
        {
            GigId = -1,
            WasAMatch = false
        };
    }
}