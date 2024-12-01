namespace Potato.Parser;

using AstNodes;

/// <summary>
///     Parses the different variable assignments and creates the necessary assignment nodes for it.
///     <example>
///         <code>
/// Integer integerVariable = 4;
/// String stringVariable = "string content";
/// Boolean booleanVariable = true;
/// Double doubleVariable = 22.33;
///         </code>
///     </example>
/// </summary>
public class PotatoVariableAssignmentParser
{
    private readonly PotatoVariableAssignmentExpressionParser _variableAssignmentExpressionParser = new();

    private (IVariableAssignmentNode IntegerNode, int ContinuationPosition) CreateIntegerAssignmentAstNodes(
        List<PotatoToken> tokens,
        int position)
    {
        IntegerVariableAssignmentNode node = new();
        int continuationPosition = position;

        if (position <= tokens.Count - 1)
        {
            if (tokens[position].TokenType != TokenTypes.Keyword_Integer)
            {
                ParserHelpers.ThrowParseException(
                    $"Expected {nameof(TokenTypes.Keyword_Integer)} but received {tokens[position].TokenType}",
                    tokens[position].LineNumber,
                    position);
            }
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.Keyword_Integer)} token type but received nothing.",
                0,
                0);
        }

        if (position + 1 <= tokens.Count)
        {
            if (tokens[position + 1].TokenType != TokenTypes.Identifier)
            {
                ParserHelpers.ThrowParseException(
                    $"Excepted {nameof(TokenTypes.Identifier)} token type, but received " +
                    $"{tokens[position + 1].TokenType}",
                    tokens[position + 1].LineNumber,
                    position + 1);
            }
            node.LeftSide = tokens[position + 1].Value;
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.Identifier)} token type, but received nothing.",
                0,
                position + 1
            );
        }

        if (position + 2 <= tokens.Count)
        {
            if (tokens[position + 2].TokenType != TokenTypes.Sign_Assignment)
            {
                ParserHelpers.ThrowParseException(
                    $"Expected {nameof(TokenTypes.Identifier)} token type, but received " +
                    $"{tokens[position + 2].TokenType}",
                    tokens[position + 2].LineNumber,
                    position + 2);
            }
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.Sign_Assignment)} token type, but received nothing.",
                0,
                position + 2
            );

        }

        // parse the right side of the equal sign where the assignment expressions live
        (ITypedExpressionNode<int> AssignmentExpressionNode, int ContinuationPosition) variableAssignmentExpression =
            _variableAssignmentExpressionParser.ParseIntegerVariableAssignmentExpression(tokens, position);


        if (position + 3 <= tokens.Count)
        {
            if (tokens[position + 3].TokenType != TokenTypes.IntegerLiteral)
            {
                ParserHelpers.ThrowParseException(
                    $"Expected {nameof(TokenTypes.IntegerLiteral)} token type, but received " +
                    $"{tokens[position + 3].TokenType}",
                    tokens[position + 3].LineNumber,
                    position + 3);
            }
            // todo: handle conversion issues
            node.Value = int.Parse(tokens[position + 3].Value);
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.IntegerLiteral)} token type, but received nothing.",
                0,
                position + 3
            );

        }

        if (position + 4 <= tokens.Count)
        {
            if (tokens[position + 4].TokenType != TokenTypes.Sign_Semicolon)
            {
                ParserHelpers.ThrowParseException(
                    $"Expected {nameof(TokenTypes.Sign_Semicolon)} token type, but received " +
                    $"{tokens[position + 4].TokenType}",
                    tokens[position + 4].LineNumber,
                    position + 4);
            }
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.Sign_Semicolon)} token type, but received nothing.",
                0,
                position + 4
            );

        }

        return (
            node,
            continuationPosition);
    }

    public (IPotatoAstNode VariableStatementNodes, int ContinuationPosition) ParseVariableAssignments(
        List<PotatoToken> tokens, int position)
    {
        switch (tokens[position].TokenType)
        {
            case TokenTypes.Keyword_Integer:
                (IVariableAssignmentNode resultNode, int ContinuationPosition)
                    integerNode = CreateIntegerAssignmentAstNodes(tokens, position);
                break;
        }

    }
}
