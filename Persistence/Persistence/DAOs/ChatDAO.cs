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

    public Task<List<Message>> GetChatHistoryAsync(int chatId)
    {
        return _dataContext.Chats.Single(c => c.Id == chatId).Messages.AsQueryable().ToListAsync();
    }

    public async Task<Chat> CreateChatAsync(List<int> userIds)
    {
       List<User> users =  await _dataContext.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
        
        Chat chatToCreate = new Chat
        {
            Participants = users
        };
        
        EntityEntry<Chat> createdChat =  _dataContext.Chats.Add(chatToCreate);
        await _dataContext.SaveChangesAsync();

        return createdChat.Entity;
    }

    public async Task<Message> SendMessageAsync(string content, int authorId, int chatId)
    {
        Message messagetoSend = new Message()
        {
            Content = content,
            AuthorId = authorId,
            ChatId = chatId
        };
        
       EntityEntry<Message> sentMessage = _dataContext.Messages.Add(messagetoSend);

       return sentMessage.Entity;
    }
}