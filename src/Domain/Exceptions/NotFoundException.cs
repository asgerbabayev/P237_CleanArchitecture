namespace Nest.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Type type, int statusCode) : base($"Status Code:{statusCode} - {type.GetType().Name} Not Found")
    {

    }
    public NotFoundException(int statusCode, string message) : base($"Status Code:{statusCode}:{message}")
    {

    }
}
