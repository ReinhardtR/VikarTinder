namespace Persistence.Exceptions.DaoExceptions;

public class DaoException : Exception
{
    public DaoException(string? message) : base(message)
    {
        Console.WriteLine(message);
    }
}