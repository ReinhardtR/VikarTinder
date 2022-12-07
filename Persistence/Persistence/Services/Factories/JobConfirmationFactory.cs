using Google.Protobuf.WellKnownTypes;
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
            Status = ToJobConfirmationStatusGrpc(jobConfirmation.Status),
            CreatedAt = jobConfirmation.CreatedAt.ToTimestamp()
        };
    }

    public static JobConfirmationStatusGrpc ToJobConfirmationStatusGrpc(JobConfirmationStatus value)
    {
        return (JobConfirmationStatusGrpc) value;
    }
    
    public static JobConfirmationStatus ToJobConfirmationStatus(JobConfirmationStatusGrpc value)
    {
        return (JobConfirmationStatus) value;
    }

    public static GetJobConfirmationResponse ToGetJobConfirmationResponse(JobConfirmation? jobConfirmation)
    {
        return jobConfirmation == null
            ? new GetJobConfirmationResponse()
            {
                JobConfirmation = null
            }
            : new GetJobConfirmationResponse()
            {
                JobConfirmation = ToJobConfirmationObject(jobConfirmation)
            };
    }
}