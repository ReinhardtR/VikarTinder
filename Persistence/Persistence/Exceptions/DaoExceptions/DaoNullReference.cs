namespace Persistence.Exceptions.DaoExceptions;

public class DaoNullReference
{
    public DaoNullReference(string? message) : base(message + " cannot be null")
    {
    }
}