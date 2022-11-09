namespace HttpClients.Services;

public class UserService
{
    private readonly Client _client;
    
    public UserService(Client client)
    {
        _client = client;   
    }
    
    public async Task<User> FindByIdAsync(long id)
    {   
        return await _client.FindByIdAsync(id);
    }
    
    public async Task<User> CreateAsync(User user)
    {
        return await _client.CreateUserAsync(user);
    }
}