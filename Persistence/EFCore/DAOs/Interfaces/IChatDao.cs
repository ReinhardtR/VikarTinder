using Persistence.Models;

namespace Persistence.DAOs;

public interface IChatDao
{
    

    public Task<Chat> CreateChatAsync(int gigId, int employerId, int substituteId);

    public Task<Message> SendMessageAsync(string content, int authorId, int chatId);

    Task<List<Chat>> GetUserChatsAsync(int userId);
    
    Task<List<Chat>> GetGigChatsAsync(int gigId);
    Task<Chat> GetChatHistoryAsync(int chatId);
}