namespace Persistence.Exceptions.ConverterExceptions;

public class ConverterException : Exception
{
    public ConverterException(string? message) : base(message)
    {
        Console.WriteLine(message);
    }
}