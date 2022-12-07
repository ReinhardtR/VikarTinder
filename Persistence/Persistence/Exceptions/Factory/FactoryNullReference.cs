namespace Persistence.Exceptions.ConverterExceptions;

public class FactoryNullReference : FactoryException
{
    public FactoryNullReference(string? message) : base(message + " cannot be null")
    {
        
    }
}