namespace Potato.Tests.Lexer;

using FluentAssertions;

using Xunit.Abstractions;

public class KeywordCases : TestBase
{
    public KeywordCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public static IEnumerable<object[]> TestData()
    {
        // String
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
            "String String",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            "String; String;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "String String String",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
            },
        };
        yield return new object[] {
            "String; String; String;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_String, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        // Integer
        yield return new object[] {
            "Integer",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
            },
        };
        yield return new object[] {
            "Integer;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Integer Integer",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Keyword_Integer, 1, ""),
            },
        };
        yield return new object[] {
            "Integer; Integer;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Integer Integer Integer",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Keyword_Integer, 1, ""),
            },
        };
        yield return new object[] {
            "Integer; Integer; Integer;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        // Boolean
        yield return new object[] {
            "Boolean",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
            },
        };
        yield return new object[] {
            "boolean",
            new List<PotatoToken> {
                new(TokenTypes.Identifier, 1, "boolean"),
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
