using Grpc.Core;
using Persistence.DAOs;
using Persistence.Models;

namespace Persistence.Services;

public class JobConfirmationServiceServer : JobConfirmationService.JobConfirmationServiceBase
{
    private readonly ILogger<JobConfirmationServiceServer> _logger;
    private readonly IJobConfirmationDAO _jobConfirmationDAO;
    
    public JobConfirmationServiceServer(ILogger<JobConfirmationServiceServer> logger, IJobConfirmationDAO jobConfirmationDAO)
    {
        _logger = logger;
        _jobConfirmationDAO = jobConfirmationDAO;
    }
    
    public override async Task<CreateJobConfirmationResponse> CreateJobConfirmation(CreateJobConfirmationRequest request, ServerCallContext context)
    {
        JobConfirmation jobConfirmation = await _jobConfirmationDAO.CreateJobConfirmationAsync(request.ChatId,request.SubstituteId,request.EmployerId);
    
        CreateJobConfirmationResponse reply = JobConfirmationFactory.ToCreateJobConfirmationResponse(jobConfirmation);
    
        return reply;
    } 
}