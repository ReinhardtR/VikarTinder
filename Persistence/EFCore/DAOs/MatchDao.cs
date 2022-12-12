using Microsoft.EntityFrameworkCore;
using Persistence.DAOs.Interfaces;
using Persistence.Dto;
using Persistence.Exceptions.DaoExceptions;
using Persistence.Models;

namespace Persistence.DAOs;

public class MatchDao : IMatchDao
{
    private readonly DataContext _dataContext;
    private readonly IChatDao _chatDao;

    public MatchDao(DataContext dataContext, IChatDao chatDao)
    {
        _dataContext = dataContext;
        _chatDao = chatDao;
    }
    
    public async Task<IdsForMatchDto> MatchingGig(ToBeMatchedDto dto)
    {
        Substitute? substitute = await GetSubstituteById(dto.UserId);
        if (substitute == null)
            throw new DaoNullReference("Substitute not found");
        
        Gig? gig = await GetGigById(dto.MatchId);
        if (gig == null)
            throw new DaoNullReference("Gig not found");
            

        //Adder den valgte gig under sig
        substitute.GigSubstitutes.Add(new GigSubstitute
        {
            Gig = gig,
            Substitute = substitute,
            WantsToMatch = dto.WantsToMatch
        });
        
        //Opdateringen og gemning
        _dataContext.Substitutes.Update(substitute);
        await _dataContext.SaveChangesAsync();

        IdsForMatchDto idsForMatch = new()
        {
            SubstituteId = substitute.Id,
            GigId = gig.Id,
            EmployerId = gig.Employer.Id,
        };
        
        IdsForMatchDto matchChecked = await CheckIfMatched(idsForMatch);
        idsForMatch.WasAMatch = dto.WantsToMatch && matchChecked.WasAMatch;
        
        return idsForMatch;
    }
    
    public async Task<IdsForMatchDto> MatchingSubstitute(ToBeMatchedDto dto)
    {
        Employer? employer = await GetEmployerById(dto.UserId);
        if (employer == null)
            throw new DaoNullReference("Employer not found");
        
        Console.WriteLine(employer.Id + " EMPLOYER");

        Substitute? substitute = await GetSubstituteById(dto.MatchId);
        if (substitute == null)
            throw new DaoNullReference("Substitute not found");
        
        employer.EmployerSubstitutes.Add(new EmployerSubstitute
        {
            Employer = employer,
            Substitute = substitute,
            WantsToMatch = dto.WantsToMatch
        });
        

        _dataContext.Employers.Update(employer);
        await _dataContext.SaveChangesAsync();
        
        IdsForMatchDto idsForMatch = new()
        {
            SubstituteId = substitute.Id,
            EmployerId = employer.Id
        };
        
        IdsForMatchDto matchChecked = await CheckIfMatched(idsForMatch);
        idsForMatch.WasAMatch = dto.WantsToMatch && matchChecked.WasAMatch;
        
        return idsForMatch;
    }

    public async Task<List<Substitute>> GetSubstitutesForMatching(int userId)
    {
        var subsQuery = _dataContext.Substitutes.AsQueryable();

        subsQuery = subsQuery.Where(substitute =>
            substitute.Employers.Any() == false ||
            substitute.Employers.All(employer => employer.Id != userId) == true);
        
        return await subsQuery.ToListAsync();
    }

    public async Task<List<Gig>> GetGigsForMatching(int id)
    {
        var gigsQuery = _dataContext.Gigs.AsQueryable();

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

        Substitute? substitute = await GetSubstituteById(id);
        if (substitute == null)
            throw new DaoNullReference("Substitute not found");

        foreach (var gigSub in substitute.GigSubstitutes)
        {
            Console.WriteLine("REAL TIME:"+ currentDateTime + "GIG SUB" +gigSub.PublicationDate +" " + gigSub.WantsToMatch);
        }

        //test
        var amountRemoved = substitute.GigSubstitutes.RemoveAll(gigsub =>
            gigsub.WantsToMatch == false && currentDateTime - gigsub.PublicationDate > span);
        Console.WriteLine(amountRemoved);
        Console.WriteLine(substitute.GigSubstitutes.Count);

        _dataContext.Substitutes.Update(substitute);
        await _dataContext.SaveChangesAsync();
    }

    private async void RemoveWhereTimerIsOutEmpSub(int id, DateTime currentDateTime, TimeSpan span)
    {
        Employer? employer = await GetEmployerById(id);
        if (employer == null)
            throw new DaoNullReference("Employer not found");
        

        foreach (var empSub in employer.EmployerSubstitutes)
        {
            Console.WriteLine("REAL TIME:"+ currentDateTime + "EMP SUB" +empSub.PublicationDate +" " + empSub.WantsToMatch);
        }

        //Test
        var amountRemoved = employer.EmployerSubstitutes.RemoveAll(empSub =>
            empSub.WantsToMatch == false && currentDateTime - empSub.PublicationDate > span);
        Console.WriteLine(amountRemoved);
        Console.WriteLine(employer.EmployerSubstitutes.Count);

        _dataContext.Employers.Update(employer);
        await _dataContext.SaveChangesAsync();
    }
    
    private async Task<CheckMatchDto> CheckMatchQuery(IdsForMatchDto dto)
    {
        //Check om employer-substitute har et element med hhv. subid og empid
        //Der på samtidig er true for begge

        var query = from gigSubs in _dataContext.Set<GigSubstitute>()
            join empSubs in _dataContext.Set<EmployerSubstitute>()
                on new { id = gigSubs.Substitute, SustituteId = dto.SubstituteId }
                equals new { id = empSubs.Substitute, SustituteId = dto.SubstituteId }
            join gigs in _dataContext.Set<Gig>().Include(gig => gig.Employer)
                on new { id = gigSubs.GigId }
                equals new { id = gigs.Id }
            select new { empSubs, gigSubs, gigs };

        foreach (var var in query)
        {
            Console.WriteLine("GIGS " + var.gigs.Id +" " + var.gigs.Employer.Id +" "
            + " EMPSUBS " + var.empSubs.EmployerId + " "+ var.empSubs.SubstituteId + " " + var.empSubs.WantsToMatch +" "
            + "GIGSUBS " + var.gigSubs.SubstituteId + " "+ var.gigSubs.WantsToMatch);
        }
        
        var result = await query.FirstOrDefaultAsync(joined =>
            joined.gigSubs.WantsToMatch == true
            && joined.empSubs.WantsToMatch == true
            && joined.empSubs.EmployerId == joined.gigs.Employer.Id
            && joined.empSubs.EmployerId == dto.EmployerId
            && joined.gigSubs.SubstituteId == dto.SubstituteId
        );

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

        if (!checkDto.WasAMatch) return checkDto;
        
        // Create chat for the match
        int substituteId = dto.SubstituteId;
        int employerId = dto.EmployerId;
        int gigId = checkDto.GigId;
        await _chatDao.CreateChatAsync(gigId, substituteId, employerId);

        return checkDto;
    }
    
    private Task<Employer?> GetEmployerById(int id)
    {
        return _dataContext.Employers.Include((e) => e.EmployerSubstitutes).FirstOrDefaultAsync(emp =>
            emp.Id == id);
    }

    private Task<Substitute?> GetSubstituteById(int id)
    {
        return _dataContext.Substitutes.Include((s) => s.GigSubstitutes).FirstOrDefaultAsync(sub => sub.Id == id);
    }
    
    private Task<Gig?> GetGigById(int id)
    {
        return _dataContext.Gigs.Include(gig => gig.Employer)
            .FirstOrDefaultAsync(gig => gig.Id == id);
    }
}