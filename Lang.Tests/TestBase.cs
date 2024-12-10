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
        List<IAssignmentStatementNode> resultVariableAssignments,
        List<IAssignmentStatementNode> expectedResultVariableAssignments)
    {
        foreach (IAssignmentStatementNode expectedAssignment in expectedResultVariableAssignments)
        {
            IAssignmentStatementNode resultAssignment = FindResultAssignmentNode(
                resultVariableAssignments,
                expectedAssignment.VariableLiteral);
            if (expectedAssignment.VariableExpressionNode != null)
            {
                resultAssignment.VariableExpressionNode.Should().NotBeNull();
            }
            CompareExpressionTrees(expectedAssignment!.VariableExpressionNode!,
                                   resultAssignment!.VariableExpressionNode!);
        }
    }

    private void CompareExpressionTrees(IExpressionNode? expected, IExpressionNode? result)
    {
        if (expected == null && result == null)
        {
            return;
        }
        if (expected == null && result != null || expected != null && result == null)
        {
            string diffInTreeErrorMessage =
                $"""
                 There is an unexpected diff between the trees.
                 expected is {expected} and result is {result}
                 """;
            true.Should().BeFalse(diffInTreeErrorMessage);
        }
        string msg = $"""
                      expected {nameof(IExpressionNode)} type is {expected.GetType()};
                      result {nameof(IExpressionNode)} type is {result.GetType()};
                      """;
        result.GetType().Should().Be(expected.GetType(), msg);

        Type? nodeTypeExpected = expected?.GetType();
        if (nodeTypeExpected == typeof(IFixExpressionNode))
        {
            IFixExpressionNode expectedNode = (IFixExpressionNode)expected;
            IFixExpressionNode resultNode = (IFixExpressionNode)result;
            CompareExpressionTrees(expectedNode.LeftSideNode, resultNode.LeftSideNode);
            CompareExpressionTrees(expectedNode.RightSideNode, resultNode.RightSideNode);
        }
        if (nodeTypeExpected == typeof(IntegerLiteralExpressionNode))
        {
            IntegerLiteralExpressionNode expectedNode = (IntegerLiteralExpressionNode)expected;
            IntegerLiteralExpressionNode resultNode = (IntegerLiteralExpressionNode)result;
            resultNode.Value.Should().Be(expectedNode.Value);
            resultNode.ExpressionNodeType.Should().Be(expectedNode.ExpressionNodeType);
            resultNode.StringTokenLiteral.Should().Be(expectedNode.StringTokenLiteral);
            resultNode.StringValueLiteral.Should().Be(expectedNode.StringValueLiteral);
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
                .Append(infixNode.TokenTypeStringLiteral)
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
