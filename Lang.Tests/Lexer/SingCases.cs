namespace Potato.Tests.Lexer;

using Xunit.Abstractions;

public class SignCases : TestBase
{
    public SignCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    // public static IEnumerable<object[]> TestData()
    // {
    //     // ;
    //     yield return new object[] {
    //         ";",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         ";;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         ";;;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "=",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "\"",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //         },
    //     };
    //     // "==" cases
    //     yield return new object[] {
    //         "==",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_DoubleEquality, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "===",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_DoubleEquality, 1, ""),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     // "!="
    //     yield return new object[] {
    //         "!=",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_BangEquality, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "!==",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_BangEquality, 1, ""),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "=!==",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_BangEquality, 1, ""),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     // "(" and ")" cases
    //     yield return new object[] {
    //         "(",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_OpenParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "((",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_OpenParentheses, 1, ""),
    //             new(TokenTypesEnum.Sign_OpenParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "=(",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_OpenParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "(=",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_OpenParentheses, 1, ""),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "= (",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_OpenParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "( =",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_OpenParentheses, 1, ""),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         ")",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_CloseParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "))",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_CloseParentheses, 1, ""),
    //             new(TokenTypesEnum.Sign_CloseParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "=)",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_CloseParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         ")=",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_CloseParentheses, 1, ""),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "= )",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_CloseParentheses, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         ") =",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_CloseParentheses, 1, ""),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
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
