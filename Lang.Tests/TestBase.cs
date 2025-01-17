namespace Potato.Tests;

using System.Text;

using AstNodes;

using FluentAssertions;

using Xunit.Abstractions;

public class TestBase
{
    protected readonly ITestOutputHelper _testOutputHelper;

    protected readonly Potato.Lexer.Lexer Lexer = new();
    protected readonly Potato.Parser.Parser Parser;

    protected TestBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Parser = new Potato.Parser.Parser(_testOutputHelper);
    }

    protected IEnumerable<string> ReadTestData(string multilineTestString)
    {
        using MemoryStream memoryStream = new();
        using StreamWriter streamWriter = new(memoryStream);

        streamWriter.Write(multilineTestString);
        streamWriter.Flush();
        memoryStream.Position = 0;

        using StreamReader streamReader = new(memoryStream);
        string aSingleLine;
        while ((aSingleLine = streamReader.ReadLine()) != null)
        {
            yield return aSingleLine;
        }
    }

    protected void CheckResult(PotatoRootAstNode result, PotatoRootAstNode expectedResult)
    {
        result.GetType().Name.Should().Be(expectedResult.GetType().Name);
        CheckVariableAssignmentNodes(result.VariableAssignments, expectedResult.VariableAssignments);
    }

    private void CheckVariableAssignmentNodes(
        List<IAssignmentStatementNode> result,
        List<IAssignmentStatementNode> expected)
    {
        if (result.Count == expected.Count && result.Count == 0)
        {
            return;
        }
        foreach (IAssignmentStatementNode expectedAssignment in expected)
        {
            IAssignmentStatementNode resultAssignment = FindResultAssignmentNode(
                result,
                expectedAssignment.VariableLiteral);
            if (expectedAssignment.VariableExpressionNode != null)
            {
                resultAssignment.VariableExpressionNode.Should().NotBeNull();
            }
            CompareExpressionTrees(expectedAssignment!.VariableExpressionNode!,
                                   resultAssignment!.VariableExpressionNode!);
        }
    }

    private void CompareExpressionTrees(IExpressionNode expected, IExpressionNode result)
    {
        string msg = $"""
                      expected {nameof(IExpressionNode)} type is {expected!.GetType()};
                      result {nameof(IExpressionNode)} type is {result!.GetType()};
                      """;
        result.GetType().Should().Be(expected.GetType(), msg);

        if (expected.GetType() == typeof(InFixExpressionNode))
        {
            InFixExpressionNode expectedNode = (InFixExpressionNode)expected;
            InFixExpressionNode resultNode = (InFixExpressionNode)result;

            if (expectedNode.LeftSideNode == null)
            {
                string nullMsgLeftSide = "Left side node expected to be null.";
                resultNode.LeftSideNode.Should().BeNull(nullMsgLeftSide);
            }
            else
            {
                resultNode.LeftSideNode!.TokenType.Should().Be(expectedNode.LeftSideNode.TokenType);
                resultNode.LeftSideNode!.ExpressionNodeType.Should().Be(expectedNode.LeftSideNode.ExpressionNodeType);
                CompareExpressionTrees(expectedNode.LeftSideNode, resultNode.LeftSideNode);
            }

            if (expectedNode.RightSideNode == null)
            {
                string nullMsgRightSide = "Right side node expected to be null.";
                resultNode.RightSideNode.Should().BeNull(null, nullMsgRightSide);
            }
            else
            {
                resultNode.RightSideNode!.TokenType.Should().Be(expectedNode.RightSideNode.TokenType);
                resultNode.RightSideNode!.ExpressionNodeType.Should().Be(expectedNode.RightSideNode.ExpressionNodeType);
                CompareExpressionTrees(expectedNode.RightSideNode, resultNode.RightSideNode);
            }
        }

        if (expected.GetType() == typeof(IntegerLiteralExpressionNode))
        {
            IntegerLiteralExpressionNode expectedNode = (IntegerLiteralExpressionNode)expected;
            IntegerLiteralExpressionNode resultNode = (IntegerLiteralExpressionNode)result;
            resultNode.Value.Should().Be(expectedNode.Value);
            resultNode.ExpressionNodeType.Should().Be(expectedNode.ExpressionNodeType);
            resultNode.ValueLiteral.Should().Be(expectedNode.ValueLiteral);
        }
    }

    private IAssignmentStatementNode FindResultAssignmentNode(
        List<IAssignmentStatementNode> resultVariableAssignments,
        string variableLiteral)
    {
        try
        {
            return resultVariableAssignments.First(w => w.VariableLiteral == variableLiteral);
        }
        catch
        {
            string msg = $"There is no assignment node in the result where the variable name is {variableLiteral}.";
            true.Should().BeFalse(msg);
        }
        throw new Exception("Bad things happen...");
    }

    protected void PrintResult(PotatoRootAstNode result)
    {
        StringBuilder builder = new();
        builder.Append(result.GetType().Name);

        builder.Append('\n');
        builder.Append('|');
        builder.Append(nameof(result.VariableAssignments));

        if (result.VariableAssignments.Count > 0)
        {
            PrintVariableAssignments(result.VariableAssignments, builder);
        }

        _testOutputHelper.WriteLine(builder.ToString());
    }

    private void PrintVariableAssignments(List<IAssignmentStatementNode> assignments, StringBuilder builder)
    {
        foreach (IAssignmentStatementNode assignment in assignments)
        {
            builder.Append('\n')
                   .Append("|-")
                   .Append(assignment.VariableLiteral);
            if (assignment.VariableExpressionNode.GetType() == typeof(InFixExpressionNode))
            {
                builder.Append(PrintVariableExpressionNodes(assignment.VariableExpressionNode, 2));

            }
            if (assignment.VariableExpressionNode.GetType() == typeof(IntegerLiteralExpressionNode))
            {
                IntegerLiteralExpressionNode integerLiteralExpressionNode = (IntegerLiteralExpressionNode)
                    assignment.VariableExpressionNode;
                builder.Append("-Value:")
                       .Append(integerLiteralExpressionNode.Value);
            }
        }
    }

    private StringBuilder PrintVariableExpressionNodes(IExpressionNode node, int depth)
    {
        StringBuilder builder = new();
        if (node.GetType() == typeof(InFixExpressionNode))
        {
            InFixExpressionNode infixNode = (InFixExpressionNode)node;
            builder
                .Append('\n')
                .Append('|')
                .Append(GenerateDepth(depth))
                .Append("type:")
                .Append(infixNode.ExpressionNodeType)
                .Append(' ')
                .Append("token type:")
                .Append(infixNode.TokenType)
                .Append('\n')
                .Append('|')
                .Append(GenerateDepth(depth))
                .Append($"{nameof(InFixExpressionNode.LeftSideNode)}")
                .Append(PrintVariableExpressionNodes(infixNode.LeftSideNode, depth + 1))
                .Append('\n')
                .Append('|')
                .Append(GenerateDepth(depth))
                .Append($"{nameof(InFixExpressionNode.RightSideNode)}")
                .Append(PrintVariableExpressionNodes(infixNode.RightSideNode, depth + 1));
        }

        if (node.GetType() == typeof(IntegerLiteralExpressionNode))
        {
            IntegerLiteralExpressionNode intLitNode = (IntegerLiteralExpressionNode)node;
            builder
                .Append('\n')
                .Append('|')
                .Append(GenerateDepth(depth))
                .Append("type:")
                .Append(intLitNode.GetType().Name)
                .Append(' ')
                .Append("value:")
                .Append(intLitNode.Value);
        }
        return builder;
    }

    private string GenerateDepth(int depth)
    {
        StringBuilder builder = new();
        for (int i = 0; i < depth; i++)
        {
            builder.Append("-|");
        }
        return builder.ToString();
    }
}
