namespace Persistence.Exceptions.ConverterExceptions;

public class FactoryException : Exception
{
    public FactoryException(string? message) : base(message)
    {
        Console.WriteLine(message);
    }
}