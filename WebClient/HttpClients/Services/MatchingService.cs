namespace HttpClients.Services;

public class  MatchingService
{
    private readonly Client _client;

    public MatchingService(Client client)
    {
        _client = client;
    }

    public async Task<SubstituteMatchingDTOs> GetSubstitutesAsync(SubstituteSearchParametersDTO parameters)
    {
        return await _client.GetSubstitutesAsync(parameters);
    }

    public Task SendSubstituteMatchRequestAsync(MatchRequestDTO request)
    {
        return _client.SubstitutesMatchRequestAsync(request);
    }

    public async Task<GigMatchingDTOs> GetGigsAsync(GigSearchParametersDTO parameters)
    {
        return await _client.GetGigsAsync(parameters);
    }

    public Task SendGigsMatchRequestAsync(MatchRequestDTO request)
    {
        return _client.GigsMatchRequestAsync(request);
    }
}