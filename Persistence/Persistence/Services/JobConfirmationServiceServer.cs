﻿using Grpc.Core;
using Persistence.DAOs;
using Persistence.Models;

namespace Persistence.Services;

public class JobConfirmationServiceServer : JobConfirmationService.JobConfirmationServiceBase
{
    private readonly ILogger<JobConfirmationServiceServer> _logger;
    private readonly IJobConfirmationDAO _jobConfirmationDao;
    
    public JobConfirmationServiceServer(ILogger<JobConfirmationServiceServer> logger, IJobConfirmationDAO jobConfirmationDao)
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
        JobConfirmation? jobConfirmation = await _jobConfirmationDao.AnswerJobConfirmationAsync(request.Id, request.ChatId, request.IsAccepted);
        
        if (jobConfirmation == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Job confirmation not found"));
        }
        
        JobConfirmationAnswerResponse reply = JobConfirmationFactory.ToJobConfirmationAnswerResponse(jobConfirmation);

        return reply;
    }
}