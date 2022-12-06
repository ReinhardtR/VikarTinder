namespace HttpClients.Services;

public class  MatchingService
{
    private readonly Client _client;

    public MatchingService(Client client)
    {
        _client = client;
    }

    public Task<SubstituteMatchingDTOs> GetSubstitutesAsync(SubstituteSearchParametersDTO parameters)
    {
        return _client.GetSubstitutesAsync(parameters);
    }

    public Task SendSubstituteMatchRequestAsync(MatchRequestDTO request)
    {
        return _client.SubstitutesMatchRequestAsync(request);
    }

    public Task<GigMatchingDTOs> GetGigsAsync(GigSearchParametersDTO parameters)
    {
        return _client.GetGigsAsync(parameters);
    }

    public Task SendGigsMatchRequestAsync(MatchRequestDTO request)
    {
        return _client.GigsMatchRequestAsync(request);
    }
}