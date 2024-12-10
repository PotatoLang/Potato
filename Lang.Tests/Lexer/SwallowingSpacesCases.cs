namespace Potato.Tests.Lexer;

using FluentAssertions;

using Xunit.Abstractions;

public class SwallowingSpacesCases : TestBase
{
    public SwallowingSpacesCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[] {
            " String",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            "String ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            " String ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            " String;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "String; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            " String; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            " String String",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            "String String ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            " String String ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            " String  String ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            " String          String ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            " String; String;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "String; String; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            " String; String; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            " String;  String; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            " String;           String; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
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
