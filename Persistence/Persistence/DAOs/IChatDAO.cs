using Persistence.Models;

namespace Persistence.DAOs;

public interface IChatDAO
{
    

    public Task<Chat> CreateChatAsync(int employerId, int substituteId);

    public Task<Message> SendMessageAsync(string content, int authorId, int chatId);

    Task<List<Chat>> GetAllChatsAsync(int userId);
    Task<Chat> GetChatHistoryAsync(int requestChatId);
}