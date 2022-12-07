namespace Persistence.Exceptions.DaoExceptions;

public class DaoNullReference : DaoException
{
    public DaoNullReference(string? message) : base(message + " cannot be null")
    {
    }
}