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
public partial class Parser
{
    private (IAssignmentStatementNode IntegerNode, int ContinuationPosition) CreateIntegerAssignmentAstNodes(
        List<PotatoToken> tokens,
        int position)
    {
        IntegerAssignmentStatementNode node = new();
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
            node.VariableLiteral = tokens[position + 1].Value;
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

        // parse the right side of the equal sign where the assignment expressions livea
        (IExpressionNode AssignmentExpressionNode, int ContinuationPosition) variableAssignmentExpression =
            ParseExpressions(tokens, position + 3);
        node.VariableExpressionNode = variableAssignmentExpression.AssignmentExpressionNode;
        return (
            node,
            continuationPosition);

        // if (variableAssignmentExpression.ContinuationPosition <= tokens.Count)
        // {
        //     if (tokens[variableAssignmentExpression.ContinuationPosition].TokenType != TokenTypes.IntegerLiteral)
        //     {
        //         ParserHelpers.ThrowParseException(
        //             $"Expected {nameof(TokenTypes.IntegerLiteral)} token type, but received " +
        //             $"{tokens[variableAssignmentExpression.ContinuationPosition].TokenType}",
        //             tokens[variableAssignmentExpression.ContinuationPosition].LineNumber,
        //             variableAssignmentExpression.ContinuationPosition);
        //     }
        //     // todo: handle conversion issues
        //     node.Value = int.Parse(tokens[variableAssignmentExpression.ContinuationPosition].Value);
        // }
        // else
        // {
        //     ParserHelpers.ThrowParseException(
        //         $"Expected {nameof(TokenTypes.IntegerLiteral)} token type, but received nothing.",
        //         0,
        //         variableAssignmentExpression.ContinuationPosition + 1
        //     );
        //
        // }
        //
        // if (variableAssignmentExpression.ContinuationPosition + 1 <= tokens.Count)
        // {
        //     if (tokens[variableAssignmentExpression.ContinuationPosition + 1].TokenType != TokenTypes.Sign_Semicolon)
        //     {
        //         ParserHelpers.ThrowParseException(
        //             $"Expected {nameof(TokenTypes.Sign_Semicolon)} token type, but received " +
        //             $"{tokens[variableAssignmentExpression.ContinuationPosition + 1].TokenType}",
        //             tokens[variableAssignmentExpression.ContinuationPosition + 1].LineNumber,
        //             variableAssignmentExpression.ContinuationPosition + 1);
        //     }
        // }
        // else
        // {
        //     ParserHelpers.ThrowParseException(
        //         $"Expected {nameof(TokenTypes.Sign_Semicolon)} token type, but received nothing.",
        //         0,
        //         variableAssignmentExpression.ContinuationPosition + 1
        //     );
        //
        // }
        //
        // return (
        //     node,
        //     continuationPosition);
    }

    public (IAssignmentStatementNode AssignmentStatementNode, int ContinuationPosition) ParseVariableAssignments(
        List<PotatoToken> tokens, int position)
    {
        switch (tokens[position].TokenType)
        {
            case TokenTypes.Keyword_Integer:
                return CreateIntegerAssignmentAstNodes(tokens, position);

            default:
                ParserHelpers.ThrowParseException($"{nameof(ParseVariableAssignments)}",
                                                  tokens[position].LineNumber,
                                                  position);
                break;
        }
        return (null, position);
    }
}
