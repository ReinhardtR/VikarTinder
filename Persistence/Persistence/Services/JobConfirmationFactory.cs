﻿using Google.Protobuf.WellKnownTypes;
using Persistence.Models;

namespace Persistence.Services;

public class JobConfirmationFactory
{
    public static CreateJobConfirmationResponse ToCreateJobConfirmationResponse(JobConfirmation jobConfirmation)
    { 
        return new CreateJobConfirmationResponse
        {
            JobConfirmation = ToJobConfirmationObject(jobConfirmation)
        };
    }

    public static JobConfirmationAnswerResponse ToJobConfirmationAnswerResponse(JobConfirmation jobConfirmation)
    {
        return new JobConfirmationAnswerResponse
        {
            JobConfirmation = ToJobConfirmationObject(jobConfirmation)
        };
    }
    
    private static JobConfirmationObject ToJobConfirmationObject(JobConfirmation jobConfirmation)
    {
        return new JobConfirmationObject
        {
            Id = jobConfirmation.Id,
            ChatId = jobConfirmation.ChatId,
            SubstituteId = jobConfirmation.SubstituteId,
            EmployerId = jobConfirmation.EmployerId,
            IsAccepted = jobConfirmation.IsAccepted,
            CreatedAt = jobConfirmation.CreatedAt.ToTimestamp()
        };
    }
    
    public static JobConfirmationStatus ToJobConfirmationStatus(bool? value)
    {
        return value switch
        {
            true => JobConfirmationStatus.Accepted,
            false => JobConfirmationStatus.Declined,
            _ => JobConfirmationStatus.Unanswered
        };
    }
}