namespace Potato.Tests;

public class TestBase
{

    protected readonly Potato.Lexer.Lexer Lexer = new();

    protected IEnumerable<string> ReadTestData(string multilineTestString)
    {
        using MemoryStream memoryStream = new();
        using StreamWriter streamWriter = new(memoryStream);

        streamWriter.Write(multilineTestString);
        streamWriter.Flush();
        memoryStream.Position = 0;

        using StreamReader streamReader = new(memoryStream);
        string aSingleLine;
        while ((aSingleLine = streamReader.ReadLine()) != null)
        {
            yield return aSingleLine;
        }
    }
}
