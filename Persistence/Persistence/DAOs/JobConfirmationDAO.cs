using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Models;

namespace Persistence.DAOs;

public class JobConfirmationDAO : IJobConfirmationDAO
{
    
    private readonly DataContext _dataContext;

    public async Task<JobConfirmation> CreateJobConfirmationAsync(int requestChatId, int requestSubstituteId, int requestEmployerId)
    {
        Chat? foundChat = _dataContext.Chats.FirstOrDefault(c => c.Id == requestChatId);
        if (foundChat == null)
            throw new Exception("Chat not found");
        
        User? foundSubstitute = _dataContext.Users.FirstOrDefault(u => u.Id == requestSubstituteId);
        if (foundSubstitute == null)
            throw new Exception("Substitute not found");
        
        User? foundEmployer = _dataContext.Users.FirstOrDefault(u => u.Id == requestEmployerId);
        if (foundEmployer == null)
            throw new Exception("Employer not found");
        
        
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
}