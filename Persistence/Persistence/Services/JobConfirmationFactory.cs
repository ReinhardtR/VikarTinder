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
                Id = jobConfirmation.id,
                ChatId = jobConfirmation.chat.Id,
                SubstituteId = jobConfirmation.substitute.Id,
                EmployerId = jobConfirmation.employer.Id,
                IsAccepted = jobConfirmation.isAccepted
            }
        };
    }
}