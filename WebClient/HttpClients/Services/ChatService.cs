namespace HttpClients.Services;

//TODO: Interface definition
public class ChatService
{
    private readonly IGeneratedClient _client;
    
    public ChatService(IGeneratedClient client)
    {
        _client = client;
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
    
    public Task<ChatOverviewDTO> GetChatOverviewAsync(int id)
    {
        return _client.GetChatOverviewAsync(id);
    }
}