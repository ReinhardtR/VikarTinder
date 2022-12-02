using Microsoft.EntityFrameworkCore;
using Persistence.DAOs.Interfaces;
using Persistence.Dto;
using Persistence.Models;

namespace Persistence.DAOs;

public class MatchDao : IMatchDao
{
    private DatabaseContext _databaseContext;

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
        else
        {
            return await DontWantGig(dto);
        }
    }

    private async Task<IdsForMatchDto> DontWantGig(ToBeMatchedDto dto)
    {
        Substitute substitute = await GetSubstituteById(dto.UserId);
        Gig gig = await GetGigById(dto.MatchId);
        
        IdsForMatchDto toBeReturned = new IdsForMatchDto()
        {
            SustituteId = substitute.Id,
            GigId = gig.Id,
            //Giver måske en fejl include fix if so
            EmployerId = gig.Employer.Id
        };

        //Adder den valgte gig under sig
        substitute.GigSubstitutes.Add(new GigSubstitute
        {
            Gig = gig,
            GigId = gig.Id,
            Substitute = substitute,
            SubstituteId = substitute.Id,
            WantsToMatch = false
        });
        //Opdateringen og gemning
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();

        return toBeReturned;
    }

    private async Task<IdsForMatchDto> MatchWithGig(ToBeMatchedDto dto)
    {
        //Finder sit eget user domæne objekt
        Substitute substitute = await GetSubstituteById(dto.UserId);
        Console.WriteLine("SUB: " + substitute.Id);

        //Finder domæneobjeketet på ejeren af gigget man har matchet
        Gig gig = await GetGigById(dto.MatchId);
        Console.WriteLine("GIG: " + gig.Id);


        IdsForMatchDto toBeReturned = new IdsForMatchDto()
        {
            SustituteId = substitute.Id,
            GigId = gig.Id,
            //Giver måske en fejl include fix if so
            EmployerId = gig.Employer.Id
        };

        //Adder den valgte gig
        substitute.GigSubstitutes.Add(new GigSubstitute
        {
            Gig = gig,
            GigId = gig.Id,
            Substitute = substitute,
            SubstituteId = substitute.Id,
            WantsToMatch = true
        });
        //Opdateringen og gemning
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Substitutes.Update(substitute);
        await _databaseContext.SaveChangesAsync();


        return toBeReturned;
    }

    public async Task<IdsForMatchDto> MatchingSubstitute(ToBeMatchedDto dto)
    {
        if (dto.WantsToMatch)
        {
            return await MatchWithSubstitute(dto);
        }
        else
        {
            return await DontWantSubstitute(dto);
        }
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

        IdsForMatchDto toBeReturned = new IdsForMatchDto()
        {
            SustituteId = substitute.Id,
            EmployerId = employer.Id
        };


        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Employers.Update(employer);

        await _databaseContext.SaveChangesAsync();

        return toBeReturned;
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

        IdsForMatchDto toBeReturned = new IdsForMatchDto()
        {
            SustituteId = substitute.Id,
            EmployerId = employer.Id
        };


        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Employers.Update(employer);

        await _databaseContext.SaveChangesAsync();

        return toBeReturned;
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

    public async Task<IdsForMatchDto> CheckIfMatched(IdsForMatchDto dto)
    {
        CheckMatchDto check = await CheckMatchQuery(dto);
        dto.WasAMatch = check.WasAMatch;
        dto.GigId = check.GigId;
        return dto;
    }
    


    private async Task<CheckMatchDto> CheckMatchQuery(IdsForMatchDto dto)
    {
        //Check om employer-substitute har et element med hhv. subid og empid
        //Der på samtidig er true for begge

        var query = from gigSubs in _databaseContext.Set<GigSubstitute>()
            join empSubs in _databaseContext.Set<EmployerSubstitute>()
                on new { id = gigSubs.Substitute, dto.SustituteId }
                equals new { id = empSubs.Substitute, dto.SustituteId }
            join gigs in _databaseContext.Set<Gig>()
                on new { id = gigSubs.GigId }
                equals new { id = gigs.Id }
            select new { empSubs, gigSubs, gigs };

        foreach (var var in query)
        {
            Console.WriteLine("GIGS" + var.gigs.Id +" " + var.gigs.Employer.Id +" "
            + " EMPSUBS" + var.empSubs.EmployerId +" " + var.empSubs.WantsToMatch +" "
            + "GIGSUBS" + var.gigSubs.WantsToMatch);
        }
        
        var result = query.FirstOrDefaultAsync(joined =>
            joined.gigSubs.WantsToMatch == joined.empSubs.WantsToMatch
            //Gigs skal måske have en iclude?
            && joined.empSubs.EmployerId == joined.gigs.Employer.Id).Result;

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
        else
        {
            Console.WriteLine("NO MATCH");
            return new CheckMatchDto
            {
                GigId = -1,
                WasAMatch = false
            };
        }
        
    }
}