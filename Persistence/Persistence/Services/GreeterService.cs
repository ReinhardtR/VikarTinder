using Grpc.Core;
using Persistence.DAOs;
using Persistence.Models;

namespace Persistence.Services;

public class GreeterService : UserService.UserServiceBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IChatDAO _chatDao;
    
    public GreeterService(ILogger<GreeterService> logger, IChatDAO chatDao)
    {
        _logger = logger;
        _chatDao = chatDao;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        Console.WriteLine("ID: " + request.Id);
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Id
        });
    }

    public override async Task<SendMessageReply> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        Message message = await _chatDao.SendMessageAsync(request.Content,request.AuthorId,request.ChatId);

        SendMessageReply reply = new SendMessageReply
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

    public override async Task<CreateChatReply> CreateChat(CreateChatRequest request, ServerCallContext context)
    {
        Chat chat = await _chatDao.CreateChatAsync(request.UserIds.ToList());

        CreateChatReply reply = new CreateChatReply
        {
            Id = chat.Id
        };

        return reply;
    }


    public override async Task<FetchChatHistoryReply> FetchChatHistory(FetchChatHistoryRequest request,
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

        FetchChatHistoryReply reply = new FetchChatHistoryReply
        {
            Messages = { messageObjects }
        };
        
        return reply;
    }
}