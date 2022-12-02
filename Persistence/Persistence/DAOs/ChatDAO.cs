using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Models;

namespace Persistence.DAOs;

public class ChatDAO : IChatDAO
{
    private readonly DataContext _dataContext;


    public ChatDAO(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

   

    public async Task<Chat> CreateChatAsync(int employerId, int substituteId)
    {
        var chat = new Chat
        {
            EmployerId = employerId,
            SubstituteId = substituteId
        };

        EntityEntry<Chat> result = await _dataContext.Chats.AddAsync(chat);
        await _dataContext.SaveChangesAsync();

        return result.Entity;
    }
    

    public async Task<Message> SendMessageAsync(string content, int authorId, int chatId)
    {
        Message messageToSend = new()
        {
            Content = content,
            AuthorId = authorId,
            ChatId = chatId
        };
        
       EntityEntry<Message> sentMessage = _dataContext.Messages.Add(messageToSend);
        await _dataContext.SaveChangesAsync();

       return sentMessage.Entity;
    }

    public Task<List<Chat>> GetAllChatsAsync(int userId)
    {
        return _dataContext.Chats.Where(c => c.EmployerId == userId || c.SubstituteId == userId).ToListAsync();
        
    }

    public Task<Chat> GetChatHistoryAsync(int requestChatId)
    {
        return _dataContext.Chats
            .Include(c => c.Messages)
            .Include(c => c.EmployerId)
            .Include(c => c.SubstituteId)
            .Include(c => c.JobConfirmations)
            .FirstOrDefaultAsync(c => c.Id == requestChatId);
        
    }

    
}