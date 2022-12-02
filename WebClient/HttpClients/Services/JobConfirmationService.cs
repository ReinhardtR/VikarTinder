namespace HttpClients.Services;

public class JobConfirmationService
{
    private readonly Client _client;
    
    public JobConfirmationService(Client client)
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