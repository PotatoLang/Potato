namespace Potato.Tests.Parser;

using AstNodes;

using Xunit.Abstractions;

public class StringVariableAssignmentCases : TestBase
{

    public StringVariableAssignmentCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [MemberData(nameof(StringAssignmentCasesTestData))]
    public void StringAssignment(string input, PotatoRootAstNode expectedResult)
    {
        IEnumerable<string> testData = ReadTestData(input);
        PotatoRootAstNode result = Parser.Parse(testData);
        PrintResult(result);
        CheckResult(result, expectedResult);
    }

    public static IEnumerable<object[]> StringAssignmentCasesTestData()
    {
        yield return [
            "String stringIdentifier = \"stringvalue\";",
            new PotatoRootAstNode {
                VariableAssignments = {
                    new StringAssignmentStatementNode {
                        VariableLiteral = "stringIdentifier",
                        VariableExpressionNode = new StringLiteralExpressionNode {
                            Value = "stringvalue",
                            StringTokenLiteral = TokenTypes.StringLiteral,
                            StringValueLiteral = "stringvalue",
                        },
                    },
                },
            },
        ];
    }
}
