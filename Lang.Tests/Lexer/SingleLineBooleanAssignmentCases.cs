namespace Potato.Tests.Lexer;

using Xunit.Abstractions;

public class SingleLineBooleanAssignmentCases : TestBase
{
    public SingleLineBooleanAssignmentCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    // public static IEnumerable<object[]> TestData()
    // {
    //     yield return new object[] {
    //         "Boolean",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier =",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier =; ",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier = 12;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.IntegerLiteral, 1, "12"),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier = true;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Value_Boolean, 1, "true"),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier = false;",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Value_Boolean, 1, "false"),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier = \"true\";",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //             new(TokenTypesEnum.StringLiteral, 1, "true"),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier = \"false\";",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //             new(TokenTypesEnum.StringLiteral, 1, "false"),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //             new(TokenTypesEnum.Sign_Semicolon, 1, ""),
    //         },
    //     };
    //     yield return new object[] {
    //         "Boolean booleanIdentifier = \"12\";",
    //         new List<PotatoToken> {
    //             new(TokenTypesEnum.Keyword_Boolean, 1, ""),
    //             new(TokenTypesEnum.Identifier, 1, "booleanIdentifier"),
    //             new(TokenTypesEnum.Sign_Assignment, 1, ""),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
    //             new(TokenTypesEnum.StringLiteral, 1, "12"),
    //             new(TokenTypesEnum.Sign_DoubleQuote, 1, ""),
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
