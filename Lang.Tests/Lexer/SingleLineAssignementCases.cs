namespace Potato.Tests.Lexer;

using FluentAssertions;

using Lexer = Potato.Lexer;

public class SingleLineAssignementCases : LexerTestBase
{
    private readonly Lexer lexer;

    public SingleLineAssignementCases()
    {
        lexer = new Lexer();
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[] {
            "var",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Var, 1, "")
            }
        };
        yield return new object[] {
            "var;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Var, 1, ""),
                new(TokenTypes.Sign_Semicolon, 1, "")
            }
        };
        yield return new object[] {
            "var identifier",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Var, 1, ""),
                new(TokenTypes.Identifier, 1, "identifier")
            }
        };
        yield return new object[] {
            "var identifier;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Var, 1, ""),
                new(TokenTypes.Identifier, 1, "identifier"),
                new(TokenTypes.Sign_Semicolon, 1, "")
            }
        };
        yield return new object[] {
            "=",
            new List<PotatoToken> {
                new(TokenTypes.Sign_Assignment, 1, "")
            }
        };
        yield return new object[] {
            ";",
            new List<PotatoToken> {
                new(TokenTypes.Sign_Semicolon, 1, "")
            }
        };
        yield return new object[] {
            "var identifier = 5;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Var, 1, ""),
                new(TokenTypes.Identifier, 1, "identifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.Value_Integer, 1, "5"),
                new(TokenTypes.Sign_Semicolon, 1, "")
            }
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Tokenize(string input, List<PotatoToken> expectedResult)
    {
        IEnumerable<string> testData = ReadTestData(input);
        List<PotatoToken> result = lexer.Lexing(testData);

        result.Should().Equal(expectedResult);
    }
}
