namespace HttpClients.Services;

//TODO: interface definition
public class JobConfirmationService
{
    private readonly IGeneratedClient _client;
    
    public JobConfirmationService(IGeneratedClient client)
    {
        _client = client;
    }

    public Task<JobConfirmationDTO> CreateJobConfirmationAsync(CreateJobConfirmationDTO dto)
    {
        return _client.CreateJobConfirmationAsync(dto);
    }

    public Task<JobConfirmationDTO> AnswerJobConfirmationAsync(AnswerJobConfirmationDTO dto)
    {
        return _client.AnswerJobConfirmationAsync(dto);
    }
}