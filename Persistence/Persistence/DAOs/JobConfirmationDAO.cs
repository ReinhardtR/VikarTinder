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
        Chat? foundChat = _dataContext.Chats.FirstOrDefault(c => c.Id == chatId);
        if (foundChat == null)
            throw new Exception("Chat not found");
        
        User? foundSubstitute = _dataContext.Users.FirstOrDefault(u => u.Id == substituteId);
        if (foundSubstitute == null)
            throw new Exception("Substitute not found");
        
        Console.WriteLine("employer id: " + employerId);
        User? foundEmployer = _dataContext.Users.FirstOrDefault(u => u.Id == employerId);
        if (foundEmployer == null)
            throw new Exception("Employer not found");


        if (foundChat.JobConfirmation != null || foundChat.JobConfirmation.IsAccepted == JobConfirmationStatus.Declined)
        {
            _dataContext.Remove(_dataContext.Chats.FirstOrDefault(c => c.Id == chatId)!.JobConfirmation);
        }
        
        
        JobConfirmation jobConfirmationToCreate = new()
        {
            Chat = foundChat,
            Substitute = foundSubstitute,
            Employer = foundEmployer 
        };

          EntityEntry<JobConfirmation> createdJobConfirmation =
          _dataContext.JobConfirmations.Add(jobConfirmationToCreate);
        
        await _dataContext.SaveChangesAsync();

        return createdJobConfirmation.Entity;
        
    }
    

    public async Task<JobConfirmation?> AnswerJobConfirmationAsync(int id, JobConfirmationStatus isAccepted)
    {
        JobConfirmation? jobConfirmation = _dataContext.JobConfirmations
            .SingleOrDefault((jc) => jc.Id == id);
        if (jobConfirmation == null) return jobConfirmation;
        
        jobConfirmation.IsAccepted = isAccepted;
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