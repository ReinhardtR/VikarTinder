using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Exceptions.DaoExceptions;
using Persistence.Models;

namespace Persistence.DAOs;

public class JobConfirmationDao : IJobConfirmationDao
{
    private readonly DataContext _dataContext;

    public JobConfirmationDao(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<JobConfirmation> CreateJobConfirmationAsync(int chatId, int substituteId, int employerId)
    {
        Chat? foundChat = await _dataContext.Chats.Include((c) => c.JobConfirmation).FirstOrDefaultAsync(c => c.Id == chatId);
        if (foundChat == null)
            throw new DaoNullReference("Chat not found");
        
        User? foundSubstitute = await _dataContext.Substitutes.FirstOrDefaultAsync(substitute => substitute.Id == substituteId);
        if (foundSubstitute == null)
            throw new DaoNullReference("Substitute not found");
        
        User? foundEmployer = await _dataContext.Employers.FirstOrDefaultAsync(employer => employer.Id == employerId);
        if (foundEmployer == null)
            throw new DaoNullReference("Employer not found");
        
        Console.WriteLine("Job confirmation: " + foundChat.JobConfirmation);
        if (foundChat.JobConfirmation != null)
        {
            _dataContext.Remove(foundChat.JobConfirmation);
        }
        
        JobConfirmation jobConfirmationToCreate = new()
        {
            Chat = foundChat,
            Substitute = foundSubstitute,
            Employer = foundEmployer 
        };
        
        EntityEntry<JobConfirmation> createdJobConfirmation = _dataContext.JobConfirmations.Add(jobConfirmationToCreate);
          
        await _dataContext.SaveChangesAsync();

        return createdJobConfirmation.Entity;
    }
    
    public async Task<JobConfirmation?> AnswerJobConfirmationAsync(int id, JobConfirmationStatus status)
    {
        JobConfirmation? jobConfirmation = _dataContext.JobConfirmations
            .SingleOrDefault((jc) => jc.Id == id);
        
        if (jobConfirmation == null) return jobConfirmation;
        
        jobConfirmation.Status = status;
        
        await  _dataContext.SaveChangesAsync();
        
        return jobConfirmation;
    }

    public Task<JobConfirmation?> GetJobConfirmationAsync(int requestId)
    {
        return _dataContext.JobConfirmations
            .Include(jc => jc.Chat)
            .Include(jc => jc.Substitute)
            .Include(jc => jc.Employer)
            .SingleOrDefaultAsync((jc) => jc.Id == requestId);
    }
}