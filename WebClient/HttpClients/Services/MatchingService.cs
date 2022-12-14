namespace HttpClients.Services;

public class  MatchingService
{
    private readonly IGeneratedClient _client;

    public MatchingService(IGeneratedClient client)
    {
        _client = client;
    }

    public Task<SubstituteMatchingDTOs> GetSubstitutesAsync(int currentUserId)
    {
        return _client.GetSubstitutesAsync(currentUserId);
    }

    public Task SendSubstituteMatchRequestAsync(MatchRequestDTO request)
    {
        return _client.SubstitutesMatchRequestAsync(request);
    }

    public Task<GigMatchingDTOs> GetGigsAsync(int currentUserId)
    {
        return _client.GetGigsAsync(currentUserId);
    }

    public Task SendGigsMatchRequestAsync(MatchRequestDTO request)
    {
        return _client.GigsMatchRequestAsync(request);
    }
}