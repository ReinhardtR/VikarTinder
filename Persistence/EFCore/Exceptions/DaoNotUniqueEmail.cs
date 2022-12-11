namespace Persistence.Exceptions.DaoExceptions;

public class DaoNotUniqueEmail : DaoException
{
    public DaoNotUniqueEmail(string? message) : base(message)
    {
    }
}