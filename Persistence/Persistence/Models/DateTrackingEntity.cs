
namespace Persistence.Models;

public abstract class DateTrackingEntity
{
    private DateTime _createdDate;
    public DateTime CreatedAt
    {
        get => _createdDate.ToUniversalTime();
        set => _createdDate = value.ToUniversalTime();
    }
    
    private DateTime _updatedDate;

    public DateTime? UpdatedAt
    {
        get => _updatedDate.ToUniversalTime();
        set => _updatedDate = value?.ToUniversalTime() ?? DateTime.UtcNow;
    }
}