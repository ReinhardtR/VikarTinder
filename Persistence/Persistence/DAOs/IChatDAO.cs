using Persistence.Models;

namespace Persistence.DAOs;

public interface IChatDAO
{
    
    public Task<List<Message>> GetChatHistoryAsync(int chatId);

    public Task<Chat> CreateChatAsync(List<int> userIds);

    public Task<Message> SendMessageAsync(string content, int authorId, int chatId);
    
}