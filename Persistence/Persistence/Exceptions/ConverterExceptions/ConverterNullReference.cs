namespace Persistence.Exceptions.ConverterExceptions;

public class ConverterNullReference : ConverterException
{
    public ConverterNullReference(string? message) : base(message + " cannot be null")
    {
        
    }
}