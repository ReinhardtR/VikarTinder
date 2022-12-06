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
    
    // TODO: merge "match gig" and "dont want to match gig" into one method
    public async Task<IdsForMatchDto> MatchingGig(ToBeMatchedDto dto)
    {
        Substitute substitute = await GetSubstituteById(dto.UserId);
        Gig gig = await GetGigById(dto.MatchId);

        //Adder den valgte gig under sig
        substitute.GigSubstitutes.Add(new GigSubstitute
        {
            Gig = gig,
            Substitute = substitute,
            WantsToMatch = dto.WantsToMatch
        });
        

        //Opdateringen og gemning
        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();
        
        
        
        IdsForMatchDto idsForMatch = new()
        {
            SustituteId = substitute.Id,
            GigId = gig.Id,
            EmployerId = gig.Employer.Id,
        };

        //TODO: Er det nødvendigt at simplificere den? :)
        idsForMatch.WasAMatch = dto.WantsToMatch ? CheckIfMatched(idsForMatch).Result.WasAMatch : false;
        
        return idsForMatch;
    }
    
    
    public async Task<IdsForMatchDto> MatchingSubstitute(ToBeMatchedDto dto)
    {
        Employer employer = await GetEmployerById(dto.UserId);
        Console.WriteLine(employer.Id + " EMPLOYER");

        Substitute substitute = await GetSubstituteById(dto.MatchId);
        
        employer.EmployerSubstitutes.Add(new EmployerSubstitute
        {
            Employer = employer,
            Substitute = substitute,
            WantsToMatch = dto.WantsToMatch
        });
        

        _databaseContext.Employers.Update(employer);
        await _databaseContext.SaveChangesAsync();
        
        IdsForMatchDto idsForMatch = new()
        {
            SustituteId = substitute.Id,
            EmployerId = employer.Id
        };
        
        idsForMatch.WasAMatch = dto.WantsToMatch ? CheckIfMatched(idsForMatch).Result.WasAMatch : false;

        return idsForMatch;
    }

    public async Task<List<Substitute>> GetSubstitutesForMatching(int userId)
    {
        var subsQuery = _databaseContext.Substitutes.AsQueryable();

        subsQuery = subsQuery.Where(substitute =>
            substitute.Employers.Any() == false ||
            substitute.Employers.All(employer => employer.Id != userId) == true);
        
        return await subsQuery.ToListAsync();
    }

    public async Task<List<Gig>> GetGigsForMatching(int id)
    {
        var gigsQuery = _databaseContext.Gigs.AsQueryable();

        gigsQuery = gigsQuery.Where(gig =>
            gig.Substitutes.Any() == false ||
            gig.Substitutes.All(substitute => substitute.Id != id) == true);
        
        return await gigsQuery.ToListAsync();
    }
    

    public async Task<IdsForMatchDto> CheckIfMatched(IdsForMatchDto dto)
    {
        CheckMatchDto check = await CheckMatchQuery(dto);
        
        dto.WasAMatch = check.WasAMatch;
        dto.GigId = check.WasAMatch ? check.GigId : 0;
        
        return dto;
    }
    
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

        Substitute? substitute = await _databaseContext.Substitutes.Include(sub =>
            sub.EmployerSubstitutes).FirstOrDefaultAsync(sub => sub.Id == id);

        foreach (var gigSub in substitute.GigSubstitutes)
        {
            Console.WriteLine("REAL TIME:"+ currentDateTime + "GIG SUB" +gigSub.PublicationDate +" " + gigSub.WantsToMatch);
        }

        //test
        var amountRemoved = substitute.GigSubstitutes.RemoveAll(gigsub =>
            gigsub.WantsToMatch == false && currentDateTime - gigsub.PublicationDate > span);
        Console.WriteLine(amountRemoved);
        Console.WriteLine(substitute.GigSubstitutes.Count);

        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();
    }

    private async void RemoveWhereTimerIsOutEmpSub(int id, DateTime currentDateTime, TimeSpan span)
    {
        Employer? employer = await _databaseContext.Employers.Include(emp =>
            emp.EmployerSubstitutes).FirstOrDefaultAsync(emp => emp.Id == id);
        

        foreach (var empSub in employer.EmployerSubstitutes)
        {
            Console.WriteLine("REAL TIME:"+ currentDateTime + "EMP SUB" +empSub.PublicationDate +" " + empSub.WantsToMatch);
        }

        //Test
        var amountRemoved = employer.EmployerSubstitutes.RemoveAll(empSub =>
            empSub.WantsToMatch == false && currentDateTime - empSub.PublicationDate > span);
        Console.WriteLine(amountRemoved);
        Console.WriteLine(employer.EmployerSubstitutes.Count);

        _databaseContext.Employers.Update(employer);
        await _databaseContext.SaveChangesAsync();
    }

    
    //TODO: Lige no returnerer den om der er en match mellem givet Sub og Emp, skal i bruge alle gigs der er matched?
    //TODO: Eller vil i give en specifik gig for at se om den er matched eller what the frank is going on
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
            Console.WriteLine("GIGS " + var.gigs.Id +" " + var.gigs.Employer.Id +" "
            + " EMPSUBS " + var.empSubs.EmployerId + " "+ var.empSubs.SubstituteId + " " + var.empSubs.WantsToMatch +" "
            + "GIGSUBS " + var.gigSubs.SubstituteId + " "+ var.gigSubs.WantsToMatch);
        }
        
        var result = query.FirstOrDefaultAsync(joined =>
            joined.gigSubs.WantsToMatch == true
            && joined.empSubs.WantsToMatch == true
            && joined.empSubs.EmployerId == joined.gigs.Employer.Id
            && joined.empSubs.EmployerId == dto.EmployerId
            && joined.gigSubs.SubstituteId == dto.SustituteId
        ).Result;

        CheckMatchDto checkDto = result != null
            ? new CheckMatchDto
            {
                GigId = result.gigs.Id,
                WasAMatch = true
            }
            : new CheckMatchDto
            {
                WasAMatch = false
            };

        if (result != null)
        {
            Console.WriteLine("MATCH: " + checkDto.WasAMatch + checkDto.GigId);
        }
        else
        {
            Console.WriteLine("NO MATCH");
        }

        return checkDto;
    }
    
    private async Task<Employer?> GetEmployerById(int id)
    {

        Employer? employer = await _databaseContext.Employers.FirstOrDefaultAsync(emp =>
            emp.Id == id);

        return employer;
    }

    private async Task<Substitute?> GetSubstituteById(int id)
    {
        Substitute? substitute = await _databaseContext.Substitutes.FirstOrDefaultAsync(sub => sub.Id == id);

        return substitute;
    }
    
    private async Task<Gig?> GetGigById(int id)
    {
        Gig? gigToReturn = await _databaseContext.Gigs.Include(gig => gig.Employer)
            .FirstOrDefaultAsync(gig => gig.Id == id);

        return gigToReturn;
    }
}