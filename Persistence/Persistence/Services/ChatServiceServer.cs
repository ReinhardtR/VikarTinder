using Google.Protobuf.Collections;
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

        SendMessageResponse reply = ChatServiceFactory.ToSendMessageResponse(message);
        
        return reply;
    }

    public override async Task<CreateChatResponse> CreateChat(CreateChatRequest request, ServerCallContext context)
    {
        Chat chat = await _chatDao.CreateChatAsync(request.UserIds.ToList());

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
        List<Message> messages = await _chatDao.GetChatHistoryAsync(request.ChatId);

        GetChatHistoryResponse reply = ChatServiceFactory.ToGetChatHistoryResponse(messages);
        
        return reply;
    }
    
    public override async Task<CreateJobConfirmationResponse> CreateJobConfirmation(CreateJobConfirmationRequest request, ServerCallContext context)
    {
        JobConfirmation jobConfirmation = await _chatDao.CreateJobConfirmationAsync(request.ChatId,request.SubstituteId,request.EmployerId);
    
        CreateJobConfirmationResponse reply = ChatServiceFactory.ToCreateJobConfirmationResponse(jobConfirmation);

        return reply;
    } 
}