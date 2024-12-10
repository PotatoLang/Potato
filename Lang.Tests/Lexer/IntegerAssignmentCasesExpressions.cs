namespace Potato.Tests.Lexer;

using FluentAssertions;

using Xunit.Abstractions;

public class IntegerAssignmentCasesExpressions : TestBase
{
    public IntegerAssignmentCasesExpressions(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[] {
            "Integer integerIdentifier = 1 + 2;",
            new List<PotatoToken> {
                new(TokenTypes.Keyword_Integer, 1, ""),
                new(TokenTypes.Identifier, 1, "integerIdentifier"),
                new(TokenTypes.Sign_Assignment, 1, ""),
                new(TokenTypes.IntegerLiteral, 1, "1"),
                new(TokenTypes.Sign_Addition, 1, ""),
                new(TokenTypes.IntegerLiteral, 1, "2"),
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
