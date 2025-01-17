namespace Potato.Tests.Parser;

using AstNodes;

using FluentAssertions;

using Potato.Parser;

public class FindContinuationNodeShould
{
    private readonly Parser _parser = new();

    public static IEnumerable<object[]> Data()
    {
        // simple infix node
        yield return [
            new InFixExpressionNode {
                IsContinuationPosition = true,
                ExpressionNodeType = ExpressionNodeType.Infix,
                TokenType = TokenTypesEnum.Identifier,
                LeftSideNode = new IntegerLiteralExpressionNode {
                    IsContinuationPosition = false,
                },
                RightSideNode = new IntegerLiteralExpressionNode {
                    IsContinuationPosition = false,
                },
            },
            new InFixExpressionNode {
                IsContinuationPosition = true,
                ExpressionNodeType = ExpressionNodeType.Infix,
                TokenType = TokenTypesEnum.Identifier,
            },
        ];
        // when there is no continuation node
        yield return [
            new InFixExpressionNode {
                IsContinuationPosition = false,
                ExpressionNodeType = ExpressionNodeType.Infix,
                TokenType = TokenTypesEnum.Identifier,
                LeftSideNode = new IntegerLiteralExpressionNode {
                    IsContinuationPosition = false,
                },
                RightSideNode = new IntegerLiteralExpressionNode {
                    IsContinuationPosition = false,
                },
            },
            null,
        ];
        yield return [
            new InFixExpressionNode {
                IsContinuationPosition = false,
                ExpressionNodeType = ExpressionNodeType.Infix,
                TokenType = TokenTypesEnum.Identifier,
                LeftSideNode = new InFixExpressionNode {
                    IsContinuationPosition = true,
                    ExpressionNodeType = ExpressionNodeType.Infix,
                    TokenType = TokenTypesEnum.Sign_Addition,
                },
                RightSideNode = new InFixExpressionNode {
                    IsContinuationPosition = false,
                    ExpressionNodeType = ExpressionNodeType.Infix,
                    TokenType = TokenTypesEnum.Sign_Division,
                },
            },
            new InFixExpressionNode {
                IsContinuationPosition = true,
                ExpressionNodeType = ExpressionNodeType.Infix,
                TokenType = TokenTypesEnum.Sign_Addition,
            },
        ];
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void ReturnContinuationNode(IExpressionNode nodes, IExpressionNode? expected)
    {
        IExpressionNode? result = _parser.FindContinuationNode(nodes);

        if (result != null && expected != null)
        {
            result.TokenType.Should().Be(expected.TokenType);
            result.ExpressionNodeType.Should().Be(expected.ExpressionNodeType);
            result.IsContinuationPosition.Should().Be(expected.IsContinuationPosition);
        }
    }
}
