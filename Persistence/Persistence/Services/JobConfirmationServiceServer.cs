using Grpc.Core;
using Persistence.DAOs;
using Persistence.Models;

namespace Persistence.Services;

public class JobConfirmationServiceServer : JobConfirmationService.JobConfirmationServiceBase
{
    private readonly ILogger<JobConfirmationServiceServer> _logger;
    private readonly IJobConfirmationDao _jobConfirmationDao;
    
    public JobConfirmationServiceServer(ILogger<JobConfirmationServiceServer> logger, IJobConfirmationDao jobConfirmationDao)
    {
        _logger = logger;
        _jobConfirmationDao = jobConfirmationDao;
    }
    
    public override async Task<CreateJobConfirmationResponse> CreateJobConfirmation(CreateJobConfirmationRequest request, ServerCallContext context)
    {
        JobConfirmation jobConfirmation = await _jobConfirmationDao.CreateJobConfirmationAsync(request.ChatId, request.SubstituteId, request.EmployerId);
    
        CreateJobConfirmationResponse reply = JobConfirmationFactory.ToCreateJobConfirmationResponse(jobConfirmation);
    
        return reply;
    }

    public override async Task<JobConfirmationAnswerResponse> AnswerJobConfirmation(
        JobConfirmationAnswerRequest request, ServerCallContext context)
    {
        JobConfirmationStatus status = JobConfirmationFactory.ToJobConfirmationStatus(request.Status);
        JobConfirmation? jobConfirmation = await _jobConfirmationDao.AnswerJobConfirmationAsync(request.Id, status);
        
        if (jobConfirmation == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Job confirmation not found"));
        }
        
        JobConfirmationAnswerResponse reply = JobConfirmationFactory.ToJobConfirmationAnswerResponse(jobConfirmation);

        return reply;
    }
    
    public override async Task<GetJobConfirmationResponse> GetJobConfirmation(GetJobConfirmationRequest request, ServerCallContext context)
    {
        JobConfirmation? jobConfirmation = await _jobConfirmationDao.GetJobConfirmationAsync(request.Id);
        
        GetJobConfirmationResponse reply = JobConfirmationFactory.ToGetJobConfirmationResponse(jobConfirmation);

        return reply;
    }
}