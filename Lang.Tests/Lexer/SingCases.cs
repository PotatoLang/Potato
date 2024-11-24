namespace Potato.Tests.Lexer;

using FluentAssertions;

public class SignCases : TestBase
{
    public static IEnumerable<object[]> TestData()
    {
        // ;
        yield return new object[] {
            ";",
            new List<PotatoToken> {
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            ";;",
            new List<PotatoToken> {
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            ";;;",
            new List<PotatoToken> {
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "=",
            new List<PotatoToken> {
                new(TokenTypes.Sign_Assignment, 1, ""),
            },
        };
        yield return new object[] {
            "\"",
            new List<PotatoToken> {
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
            },
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Tokenize(string input, List<PotatoToken> expectedResult)
    {
        IEnumerable<string> testData = ReadTestData(input);
        List<PotatoToken> result = Lexer.Lexing(testData);

        result.Should().Equal(expectedResult);
    }
}
