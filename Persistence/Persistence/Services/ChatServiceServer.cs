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

    public override async Task<GetEmployerGigsResponse> GetEmployerGigs(GetEmployerGigsRequest request,
        ServerCallContext context)
    {
        List<Gig> gigs = await _chatDao.GetEmployerGigs(request.EmployerId);
        return ChatServiceFactory.ToGetEmployerGigsResponse(gigs);
    }
    
    public override async Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        Message message = await _chatDao.SendMessageAsync(request.Content,request.AuthorId,request.ChatId);
        return ChatServiceFactory.ToSendMessageResponse(message);
    }

    public override async Task<CreateChatResponse> CreateChat(CreateChatRequest request, ServerCallContext context)
    {
        Chat chat = await _chatDao.CreateChatAsync(request.GigId ,request.Employer.Id,request.Substitute.Id);
        return ChatServiceFactory.ToCreateChatResponse(chat);
        
    }

    public override async Task<GetUserChatsResponse> GetUserChats(GetUserChatsRequest request, ServerCallContext context)
    {
        List<Chat> chats = await _chatDao.GetUserChatsAsync(request.UserId);
        return ChatServiceFactory.ToGetUserChatsResponse(chats);
    }
    
    public override async Task<GetGigChatsResponse> GetGigChats(GetGigChatsRequest request, ServerCallContext context)
    {
        List<Chat> chats = await _chatDao.GetGigChatsAsync(request.GigId);
        return ChatServiceFactory.ToGigChatsResponse(chats);
    }

    public override async Task<GetChatHistoryResponse> GetChatHistory(GetChatHistoryRequest request,
        ServerCallContext context)
    {
        Chat chat = await _chatDao.GetChatHistoryAsync(request.ChatId);
        return ChatServiceFactory.ToGetChatHistoryResponse(chat);
    }
}