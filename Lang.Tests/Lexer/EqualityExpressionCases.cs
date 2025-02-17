namespace Potato.Tests.Lexer;

using Xunit.Abstractions;

public class EqualityExpressionCases : TestBase
{
    public EqualityExpressionCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    // public static IEnumerable<object[]> TestData()
    // {
    //     yield return new object[] {
    //         "identifierA == identifierB",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Identifier, TokenTypes.Identifier, 1, "identifierA"),
    //             new(TokenTypesEnum.Sign_DoubleEquality, TokenTypes.Sign_DoubleEquality, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, TokenTypes.Identifier, "identifierB"),
    //         },
    //     };
    //     yield return new object[] {
    //         "(identifierA == identifierB)",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_OpenParentheses, TokenTypes.Sign_OpenParentheses, 1, ""),
    //             new(TokenTypesEnum.Identifier, TokenTypes.Identifier, 1, "identifierA"),
    //             new(TokenTypesEnum.Sign_DoubleEquality, TokenTypes.Sign_DoubleEquality, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, TokenTypes.Identifier, "identifierB"),
    //             new(TokenTypesEnum.Sign_CloseParentheses, TokenTypes.Sign_CloseParentheses, 1, ""),
    //         },
    //     };
    // }
    //
    // [Theory]
    // [MemberData(nameof(TestData))]
    // public void Tokenize(string input, List<PotatoToken> expectedResult)
    // {
    //     IEnumerable<string> testData = ReadTestData(input);
    //     List<PotatoToken> result = Lexer.Lexing(testData);
    //
    //     result.Should().Equal(expectedResult);
    // }
}
