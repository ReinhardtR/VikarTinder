namespace HttpClients.Services;

public class MatchingService
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

    public async Task<MatchValidationDTO> SendSubstituteMatchRequestAsync(MatchRequestDTO request)
    {
        return await _client.SubstitutesMatchRequestAsync(request);
    }

    public async Task<GigMatchingDTOs> GetGigsAsync(GigSearchParametersDTO parameters)
    {
        return await _client.GetGigsAsync(parameters);
    }

    public async Task<MatchValidationDTO> SendGigsMatchRequestAsync(MatchRequestDTO request)
    {
        return await _client.GigsMatchRequestAsync(request);
    }
}