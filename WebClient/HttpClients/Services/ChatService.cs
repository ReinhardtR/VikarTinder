namespace HttpClients.Services;


public class ChatService
{
    private readonly IGeneratedClient _client;
    
    public ChatService(IGeneratedClient client)
    {
        _client = client;
    }

    public Task<EmployerGigsDTO> GetEmployerGigsAsync(int employerId)
    {
        return _client.GetEmployerGigsAsync(employerId);
    }

    public Task<BasicChatDTO> CreateChatAsync(CreateChatDTO dto)
    {
        return _client.CreateChatAsync(dto);
    }

    public Task<MessageDTO> SendMessageAsync(SendMessageDTO dto)
    {
        Console.WriteLine(dto.Content);
        return _client.SendMessageAsync(dto);
    }
    
    public async Task<ChatHistoryDTO?> GetChatHistoryAsync(int id)
    {
        ChatHistoryDTO chatHistoryDto = await _client.GetChatHistoryAsync(id);

        if (chatHistoryDto.JobConfirmation.Id == 0)
        {
            chatHistoryDto.JobConfirmation = null;
        }

        return chatHistoryDto;
    }
    
    public Task<ChatOverviewDTO> GetChatOverviewByUserAsync(int userId)
    {
        return _client.GetChatOverviewByUserAsync(userId);
    }
    
    public Task<ChatOverviewDTO> GetChatOverviewByGigAsync(int gigId)
    {
        return _client.GetChatOverviewByGigAsync(gigId);
    }
}