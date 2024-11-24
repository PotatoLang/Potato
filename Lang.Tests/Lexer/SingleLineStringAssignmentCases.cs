namespace Potato.Tests.Lexer;

using FluentAssertions;

public class SingleLineStringAssignmentCases : TestBase
{
    public static IEnumerable<object[]> TestData()
    {
        yield return new object[] {
            "String",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            "String;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "String identifier",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Identifier, 1, "identifier"),
            },
        };
        yield return new object[] {
            "String identifier;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Identifier, 1, "identifier"),
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
            ";",
            new List<PotatoToken> {
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "String identifier = \"5\";",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Identifier, 1, "identifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
                new(TokenTypes.Value_String, 1, "5"),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "String identifier = 5;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Identifier, 1, "identifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Value_Integer, 1, "5"),
                new(TokenTypes.Sign_Semicolon, 1, ""),
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
