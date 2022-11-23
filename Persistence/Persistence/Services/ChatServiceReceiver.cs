using Grpc.Core;
using Persistence.DAOs;
using Persistence.Models;

namespace Persistence.Services;

public class ChatServiceReceiver : ChatService.ChatServiceBase
{
    private readonly ILogger<ChatServiceReceiver> _logger;
    private readonly IChatDAO _chatDao;
    
    public ChatServiceReceiver(ILogger<ChatServiceReceiver> logger, IChatDAO chatDao)
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