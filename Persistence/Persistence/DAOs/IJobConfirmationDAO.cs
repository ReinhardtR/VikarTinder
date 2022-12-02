using Persistence.Models;

namespace Persistence.DAOs;

public interface IJobConfirmationDAO
{

    Task<JobConfirmation> CreateJobConfirmationAsync(int requestChatId, int requestSubstituteId, int requestEmployerId);
    Task<JobConfirmation?> AnswerJobConfirmationAsync(int requestId, int requestChatId, bool requestIsAccepted);
    Task<JobConfirmation?> GetJobConfirmationAsync(int requestId);
}