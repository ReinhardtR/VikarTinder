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
            ChatId = jobConfirmation.Chat.Id,
            SubstituteId = jobConfirmation.Substitute.Id,
            EmployerId = jobConfirmation.Employer.Id,
            IsAccepted = jobConfirmation.IsAccepted,
            CreatedAt = jobConfirmation.CreatedAt
        };
    }
}