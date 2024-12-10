namespace Potato.Tests.Lexer;

using FluentAssertions;

using Xunit.Abstractions;

public class EqualityExpressionCases : TestBase
{
    public EqualityExpressionCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[] {
            "identifierA == identifierB",
            new List<PotatoToken> {
                new(TokenTypes.Identifier, 1, "identifierA"),
                new(TokenTypes.Sign_DoubleEquality, 1, ""),
                new(TokenTypes.Identifier, 1, "identifierB"),
            },
        };
        yield return new object[] {
            "(identifierA == identifierB)",
            new List<PotatoToken> {
                new(TokenTypes.Sign_OpenParentheses, 1, ""),
                new(TokenTypes.Identifier, 1, "identifierA"),
                new(TokenTypes.Sign_DoubleEquality, 1, ""),
                new(TokenTypes.Identifier, 1, "identifierB"),
                new(TokenTypes.Sign_CloseParentheses, 1, ""),
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
