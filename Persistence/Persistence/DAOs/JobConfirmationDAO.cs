using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Models;

namespace Persistence.DAOs;

public class JobConfirmationDAO : IJobConfirmationDAO
{
    private readonly DataContext _dataContext;

    public JobConfirmationDAO(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<JobConfirmation> CreateJobConfirmationAsync(int chatId, int substituteId, int employerId)
    {
        Chat? foundChat = _dataContext.Chats.Include((c) => c.JobConfirmation).FirstOrDefault(c => c.Id == chatId);
        if (foundChat == null)
            throw new Exception("Chat not found");
        
        User? foundSubstitute = _dataContext.Users.FirstOrDefault(u => u.Id == substituteId);
        if (foundSubstitute == null)
            throw new Exception("Substitute not found");
        
        User? foundEmployer = _dataContext.Users.FirstOrDefault(u => u.Id == employerId);
        if (foundEmployer == null)
            throw new Exception("Employer not found");
        
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