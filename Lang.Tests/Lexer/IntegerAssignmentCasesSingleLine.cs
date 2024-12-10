namespace Potato.Tests.Lexer;

using FluentAssertions;

using Xunit.Abstractions;

public class IntegerAssignmentCasesSingleLine : TestBase
{
    public IntegerAssignmentCasesSingleLine(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public static IEnumerable<object[]> TestData()
    {
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
            "Integer integerIdentifier",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Identifier, 1, "integerIdentifier"),
            },
        };
        yield return new object[] {
            "Integer integerIdentifier;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Identifier, 1, "integerIdentifier"),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Integer integerIdentifier =",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Identifier, 1, "integerIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
            },
        };
        yield return new object[] {
            "Integer integerIdentifier =; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Identifier, 1, "integerIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Integer integerIdentifier = 12;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Identifier, 1, "integerIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.IntegerLiteral, 1, "12"),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Integer integerIdentifier = \"12\";",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Identifier, 1, "integerIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
                new(TokenTypes.Value_String, 1, "12"),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
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
