namespace Potato.Tests.Parser.Variable.Expressions.Integer;

using AstNodes;

using Xunit.Abstractions;

public class AssigmentWithNoExpression : TestBase
{

    public AssigmentWithNoExpression(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void AssignLiteralValue()
    {
        string input = "Integer integerIdentifier = 1;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new IntegerLiteralExpressionNode {
                        Value = 1,
                        ValueLiteral = "1",
                        ExpressionNodeType = ExpressionNodeType.LiteralValue,
                    },
                },
            },
        };
        PotatoRootAstNode result = Parser.Parse(testData);
        PrintResult(result);
        CheckResult(result, expectedResult);
    }
}
