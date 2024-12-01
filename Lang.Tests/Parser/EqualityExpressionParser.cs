namespace Potato.Tests.Parser;

using AstNodes;

using FluentAssertions;

public class EqualityExpressionParser : TestBase
{
    public static IEnumerable<object[]> CorrectCasesData()
    {
        yield return new object[] {
            "111 == 111;",
            new PotatoBaseAstNode {
                Nodes = new List<IPotatoAstNode> {
                    new IntegerTypedEqualityExpressionAstNode {
                        RightSide = 111,
                        LeftSide = 111,
                        Operation = TokenTypes.Sign_DoubleEquality,
                        Result = true,
                    },
                },
            },
        };
    }

    [Theory]
    [MemberData(nameof(CorrectCasesData))]
    public void CorrectCases(string input, PotatoBaseAstNode expectedResult)
    {
        IEnumerable<string> sourceCode = ReadTestData(input);
        PotatoBaseAstNode result = Parser.Parse(sourceCode);

        result.NodeType.Should().Be(expectedResult.NodeType);
        result.Nodes[0].Should().BeOfType<IntegerTypedEqualityExpressionAstNode>();
        IntegerTypedEqualityExpressionAstNode resultNode = (IntegerTypedEqualityExpressionAstNode)result.Nodes[0];
        IntegerTypedEqualityExpressionAstNode expectedNode =
            (IntegerTypedEqualityExpressionAstNode)expectedResult.Nodes[0];
        resultNode.RightSide.Should().Be(expectedNode.RightSide);
        resultNode.LeftSide.Should().Be(expectedNode.LeftSide);
        resultNode.Result.Should().Be(expectedNode.Result);
        resultNode.Operation.Should().Be(expectedNode.Operation);
        resultNode.NodeType.Should().Be(expectedNode.NodeType);
    }
}
