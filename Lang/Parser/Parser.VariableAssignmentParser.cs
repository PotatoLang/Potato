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
            if (tokens[position].TokenType != TokenTypesEnum.Keyword_Integer)
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
            if (tokens[position + 1].TokenType != TokenTypesEnum.Identifier)
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
            if (tokens[position + 2].TokenType != TokenTypesEnum.Sign_Assignment)
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
        List<PotatoToken> tokensPartial = GetTokensUntil(tokens, position + 3, TokenTypesEnum.Sign_Semicolon);
        (IExpressionNode AssignmentExpressionNode, int ContinuationPosition) variableAssignmentExpression =
            ParseExpressions(tokensPartial);
        node.VariableExpressionNode = variableAssignmentExpression.AssignmentExpressionNode;
        return (
            node,
            continuationPosition);
    }

    public (IAssignmentStatementNode AssignmentStatementNode, int ContinuationPosition) ParseVariableAssignments(
        List<PotatoToken> tokens, int position)
    {
        switch (tokens[position].TokenType)
        {
            case TokenTypesEnum.Keyword_Integer:
                return CreateIntegerAssignmentAstNodes(tokens, position);

            case TokenTypesEnum.Keyword_String:
                return CreateStringAssignmentAstNode(tokens, position);

            default:
                ParserHelpers.ThrowParseException($"{nameof(ParseVariableAssignments)}",
                                                  tokens[position].LineNumber,
                                                  position);
                break;
        }
        return (null, position);
    }

    private (IAssignmentStatementNode AssignmentStatementNode, int ContinuationPosition) CreateStringAssignmentAstNode(
        List<PotatoToken> tokens,
        int position)
    {
        StringAssignmentStatementNode node = new();
        int continuationPosition = position;
        int tokensLength = tokens.Count - 1;

        // checking String presence
        if (continuationPosition < tokensLength)
        {
            if (tokens[continuationPosition].TokenType != TokenTypesEnum.Keyword_String)
            {
                ParserHelpers.ThrowParseException(
                    $"Expected {nameof(TokenTypes.Keyword_String)}, but received {tokens[continuationPosition].TokenType}",
                    tokens[position - 1].LineNumber,
                    continuationPosition
                );
            }
            continuationPosition++;
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.Keyword_String)}, but there is no further characters in the code.",
                tokens[position - 1].LineNumber,
                position
            );
        }

        if (continuationPosition < tokensLength)
        {
            if (tokens[continuationPosition].TokenType != TokenTypesEnum.Identifier)
            {
                ParserHelpers.ThrowParseException(
                    $"Expected {nameof(TokenTypes.Identifier)}, but received {tokens[continuationPosition].TokenType}",
                    tokens[continuationPosition].LineNumber,
                    continuationPosition
                );
            }
            continuationPosition++;
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.Identifier)}, but there is no further characters in the code.",
                tokens[continuationPosition].LineNumber,
                continuationPosition
            );

        }

        if (continuationPosition < tokensLength)
        {
            if (tokens[continuationPosition].TokenType != TokenTypesEnum.Sign_Assignment)
            {
                ParserHelpers.ThrowParseException(
                    $"Expected {nameof(TokenTypes.Sign_Assignment)}, but received {tokens[continuationPosition].TokenType}",
                    tokens[continuationPosition].LineNumber,
                    continuationPosition
                );
            }
            continuationPosition++;
        }
        else
        {
            ParserHelpers.ThrowParseException(
                $"Expected {nameof(TokenTypes.Sign_Assignment)}, but there is no further characters in the code.",
                tokens[continuationPosition].LineNumber,
                continuationPosition
            );
        }

        List<PotatoToken> stringAssignmentExpressionTokens = GetTokensUntil(
            tokens,
            continuationPosition,
            TokenTypesEnum.Sign_Semicolon);

        (IExpressionNode AssignmentExpressionNode, int ContinuationPosition) variableAssignmentExpression =
            ParseExpressions(stringAssignmentExpressionTokens);
        node.VariableExpressionNode = variableAssignmentExpression.AssignmentExpressionNode;
        return (
            node,
            variableAssignmentExpression.ContinuationPosition);

    }

    private List<PotatoToken> GetTokensUntil(List<PotatoToken> tokens, int position, TokenTypesEnum delimiter)
    {
        List<PotatoToken> tokensPartial = new();
        for (int i = position; i < tokens.Count; i++)
        {
            if (tokens[i].TokenType == delimiter)
            {
                tokensPartial.Add(tokens[i]);
                break;
            }
            tokensPartial.Add(tokens[i]);
        }
        return tokensPartial;
    }
}
