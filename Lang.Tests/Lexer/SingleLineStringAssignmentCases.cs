namespace Potato.Tests.Lexer;

using Xunit.Abstractions;

public class SingleLineStringAssignmentCases : TestBase
{
    public SingleLineStringAssignmentCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    // public static IEnumerable<object[]> TestData()
    // {
    //     yield return new object[] {
    //         "String",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_String, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "String;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_String, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "String identifier",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_String, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "identifier"),
    //         },
    //     };
    //     yield return new object[] {
    //         "String identifier;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_String, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "identifier"),
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
    //         ";",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "String identifier = \"5\";",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_String, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "identifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //             new(TokenTypesEnum.StringLiteral, 1, "5"),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "String identifier = 5;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_String, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "identifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.IntegerLiteral, 1, "5"),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
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
