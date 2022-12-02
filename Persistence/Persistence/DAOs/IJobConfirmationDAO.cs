using Persistence.Models;

namespace Persistence.DAOs;

public interface IJobConfirmationDAO
{
    Task<JobConfirmation> CreateJobConfirmationAsync(int requestChatId, int requestSubstituteId, int requestEmployerId);
}