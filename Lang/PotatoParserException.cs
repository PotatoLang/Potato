namespace Potato;

public class PotatoParserException : Exception
{
    public PotatoParserException()
    {
    }

    public PotatoParserException(string? message) : base(message)
    {
    }

    public PotatoParserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
