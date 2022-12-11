using Google.Protobuf.Collections;
using Grpc.Core;
using Persistence.DAOs;
using Persistence.Models;

namespace Persistence.Services;

public class ChatServiceServer : ChatService.ChatServiceBase
{
    private readonly ILogger<ChatServiceServer> _logger;
    private readonly IChatDao _chatDao;
    
    
    public ChatServiceServer(ILogger<ChatServiceServer> logger, IChatDao chatDao)
    {
        _logger = logger;
        _chatDao = chatDao;
    }
    
    public override async Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        Message message = await _chatDao.SendMessageAsync(request.Content,request.AuthorId,request.ChatId);

        SendMessageResponse reply = ChatServiceFactory.ToSendMessageResponse(message);
        
        return reply;
    }

    public override async Task<CreateChatResponse> CreateChat(CreateChatRequest request, ServerCallContext context)
    {
        Chat chat = await _chatDao.CreateChatAsync(request.Employer.Id,request.Substitute.Id);

        CreateChatResponse reply = ChatServiceFactory.ToCreateChatResponse(chat);

        return reply;
        
    }

    public override async Task<GetChatOverviewResponse> GetChatOverview(GetChatOverviewRequest request, ServerCallContext context)
    {
        List<Chat> chats = await _chatDao.GetAllChatsAsync(request.UserId);

        Console.WriteLine("Chats: " + chats.Count);
        
        GetChatOverviewResponse reply = ChatServiceFactory.ToGetChatOverviewResponse(chats);

        return reply;
    }
    
    
    public override async Task<GetChatHistoryResponse> GetChatHistory(GetChatHistoryRequest request,
        ServerCallContext context)
    {
        Chat chat = await _chatDao.GetChatHistoryAsync(request.ChatId);
        
        GetChatHistoryResponse reply = ChatServiceFactory.ToGetChatHistoryResponse(chat);

        return reply;
    }
}