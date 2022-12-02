using Persistence.Models;

namespace Persistence.DAOs;

public interface IJobConfirmationDAO
{
    Task<JobConfirmation> CreateJobConfirmationAsync(int chatId, int substituteId, int employerId);
    Task<JobConfirmation?> AnswerJobConfirmationAsync(int id, JobConfirmationStatus isAccepted);
}