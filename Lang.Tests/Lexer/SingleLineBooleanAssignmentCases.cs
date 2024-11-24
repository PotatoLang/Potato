namespace Potato.Tests.Lexer;

using FluentAssertions;

public class SingleLineBooleanAssignmentCases : TestBase
{
    public static IEnumerable<object[]> TestData()
    {
        yield return new object[] {
            "Boolean",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier =",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier =; ",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier = 12;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Value_Integer, 1, "12"),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier = true;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Value_Boolean, 1, "true"),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier = false;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Value_Boolean, 1, "false"),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier = \"true\";",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
                new(TokenTypes.Value_String, 1, "true"),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier = \"false\";",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
                new(TokenTypes.Value_String, 1, "false"),
                new(TokenTypes.Sign_DoubleQuote, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, ""),
            },
        };
        yield return new object[] {
            "Boolean booleanIdentifier = \"12\";",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Boolean, 1, ""),
                new(TokenTypes.Identifier, 1, "booleanIdentifier"),
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
