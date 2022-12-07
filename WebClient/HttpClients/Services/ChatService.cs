namespace HttpClients.Services;

//TODO: Interface definition
public class ChatService
{
    private readonly Client _client;
    
    public ChatService(Client client)
    {
        _client = client;
    }

    public Task<BasicChatDTO> CreateChatAsync(CreateChatDTO dto)
    {
        return _client.CreateChatAsync(dto);
    }

    public Task<MessageDTO> SendMessageAsync(SendMessageDTO dto)
    {
        return _client.SendMessageAsync(dto);
    }
    
    public Task<ChatHistoryDTO?> GetChatHistoryAsync(int id)
    {
        return _client.GetChatHistoryAsync(id);;
    }
    
    public Task<ChatOverviewDTO> GetChatOverviewAsync(int id)
    {
        return _client.GetChatOverviewAsync(id);
    }
}