using Grpc.Core;
using Persistence.DAOs;
using Persistence.Models;

namespace Persistence.Services;

public class ChatServiceServer : ChatService.ChatServiceBase
{
    private readonly ILogger<ChatServiceServer> _logger;
    private readonly IChatDAO _chatDao;
    
    public ChatServiceServer(ILogger<ChatServiceServer> logger, IChatDAO chatDao)
    {
        _logger = logger;
        _chatDao = chatDao;
    }

  

    public override async Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        Message message = await _chatDao.SendMessageAsync(request.Content,request.AuthorId,request.ChatId);

        SendMessageResponse reply = new SendMessageResponse()
        {
            Message = new MessageObject()
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                ChatId = message.ChatId,
                Content = message.Content
            }
        };
        
        return reply;
    }

    public override async Task<CreateChatResponse> CreateChat(CreateChatRequest request, ServerCallContext context)
    {
        Chat chat = await _chatDao.CreateChatAsync(request.UserIds.ToList());

        CreateChatResponse reply = new CreateChatResponse()
        {
            Id = chat.Id
        };

        return reply;
    }

    public override async Task<GetAllChatsResponse> GetAllChats(GetAllChatsRequest request, ServerCallContext context)
    {
        List<Chat> chats = await _chatDao.GetAllChatsAsync(request.UserId);

        GetAllChatsResponse reply = new GetAllChatsResponse();

        foreach (Chat chat in chats)
        {
            reply.Chats.Add(new ChatObject()
            {
                Id = chat.Id,
                UserIds = {chat.Participants.Select(p => p.Id)},
                Messages = { chat.Messages.Select(message => new MessageObject()
                {
                    Id = message.Id,
                    AuthorId = message.AuthorId,
                    ChatId = message.ChatId,
                    Content = message.Content
                })}
            });
        }

        return reply;
    }
    
    
    public override async Task<GetChatHistoryResponse> GetChatHistory(GetChatHistoryRequest request,
        ServerCallContext context)
    {
        List<Message> messages = await _chatDao.GetChatHistoryAsync(request.ChatId);

        IEnumerable<MessageObject> messageObjects = messages.Select((m) => new MessageObject
        {
            Id = m.Id,
            Content = m.Content,
            AuthorId = m.AuthorId,
            ChatId = m.ChatId
        });

        GetChatHistoryResponse reply = new GetChatHistoryResponse()
        {
            Messages = { messageObjects }
        };
        
        return reply;
    }
}