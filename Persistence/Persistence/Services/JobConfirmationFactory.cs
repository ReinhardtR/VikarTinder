using Persistence.Models;

namespace Persistence.Services;

public class JobConfirmationFactory
{
    public static CreateJobConfirmationResponse ToCreateJobConfirmationResponse(JobConfirmation jobConfirmation)
    { 
        return new CreateJobConfirmationResponse
        {
            JobConfirmation = new JobConfirmationObject
            {   
                Id = jobConfirmation.Id,
                ChatId = jobConfirmation.Chat.Id,
                SubstituteId = jobConfirmation.Substitute.Id,
                EmployerId = jobConfirmation.Employer.Id,
                IsAccepted = jobConfirmation.IsAccepted
                
            }
        };
    }
}