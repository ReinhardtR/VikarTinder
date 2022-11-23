namespace HttpClients.Services;

public class ChatService
{
    private readonly Client _client;
    
    public ChatService(Client client)
    {
        _client = client;
    }

    public async Task<ChatDTO> CreateChatAsync(CreateChatDTO dto)
    {
        return await _client.CreateChatAsync(dto);
    }

    public async Task<MessageDTO> SendMessageAsync(SendMessageDTO dto)
    {
        return await _client.SendMessageAsync(dto);
    }
    
    public async Task<ChatHistoryDTO> GetChatHistoryAsync(GetChatHistoryDTO dto)
    {
        return await _client.GetChatHistoryAsync(dto);
    }
}