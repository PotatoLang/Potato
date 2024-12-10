namespace Potato.Parser;

using AstNodes;

using Microsoft.Extensions.Logging;

public partial class Parser
{
    private (IExpressionNode AssignmentExpressionNode, int ContinuationPosition)
        ParseExpressions(
            List<PotatoToken> tokens,
            int position
        ) => BuildExpressionTree(tokens, position, null, ExpressionPrecedences.Lowest);

    private (IExpressionNode ExpressionNode, int ContinuationPosition) BuildExpressionTree(
        List<PotatoToken> tokens,
        int position,
        IExpressionNode? expressionNode,
        ExpressionPrecedences precedence)
    {

        _logger.LogInformation("==== New token ====");
        _logger.LogInformation("=== Input ===");
        _logger.LogInformation("== Token type: {Token}", tokens[position].TokenType);
        _logger.LogInformation("== Token literal: {Token}", tokens[position].Value);
        _logger.LogInformation("== Received node type: {NodeType}", expressionNode?.GetType());
        _logger.LogInformation("== Received precedence: {NodeType}", precedence);


        PotatoToken actualToken = tokens[position];
        ExpressionPrecedences actualTokenPrecedence = DeterminePrecedence(actualToken);

        if (actualTokenPrecedence > precedence && actualToken.TokenType != TokenTypes.Sign_Semicolon)
        {
            // higher precedence means that a new parent node will be created
            // create a new expression node which will be the parent of the existing node
            // add existing node to left
            switch (actualToken.TokenType)
            {
                case TokenTypes.Sign_Addition:
                    InFixExpressionNode additionInFixExpressionNode = CreateInfixExpressionNode(actualToken);
                    if (expressionNode == null)
                    {
                        // syntax error
                        ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                    }
                    additionInFixExpressionNode.LeftSideNode = expressionNode;

                    _logger.LogInformation("=== created node ===");
                    _logger.LogInformation("== node type: {NodeType}", additionInFixExpressionNode.GetType());
                    _logger.LogInformation("== node.LeftSideNode type: {NodeType}",
                                           additionInFixExpressionNode?.LeftSideNode?.GetType());
                    _logger.LogInformation("== node.RightSideNode type: {NodeType}",
                                           additionInFixExpressionNode?.RightSideNode?.GetType());
                    _logger.LogInformation("== previous precedence: {P}", precedence);
                    _logger.LogInformation("== token precedence: {P}", actualTokenPrecedence);
                    _logger.LogInformation("\n \n");
                    return BuildExpressionTree(tokens, position + 1, additionInFixExpressionNode,
                                               actualTokenPrecedence);

                case TokenTypes.Sign_Subtraction:
                    InFixExpressionNode subtractionInFixExpressionNode = CreateInfixExpressionNode(actualToken);
                    if (expressionNode == null)
                    {
                        // syntax error
                        ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                    }
                    subtractionInFixExpressionNode.LeftSideNode = expressionNode;

                    _logger.LogInformation("=== created node ===");
                    _logger.LogInformation("== node type: {NodeType}", subtractionInFixExpressionNode.GetType());
                    _logger.LogInformation("== node.LeftSideNode type: {NodeType}",
                                           subtractionInFixExpressionNode?.LeftSideNode?.GetType());
                    _logger.LogInformation("== node.RightSideNode type: {NodeType}",
                                           subtractionInFixExpressionNode?.RightSideNode?.GetType());
                    _logger.LogInformation("== previous precedence: {P}", precedence);
                    _logger.LogInformation("== token precedence: {P}", actualTokenPrecedence);
                    _logger.LogInformation("\n \n");
                    return BuildExpressionTree(tokens, position + 1, subtractionInFixExpressionNode,
                                               actualTokenPrecedence);

                case TokenTypes.Sign_Multiplication:
                    InFixExpressionNode multiplicationInFixExpressionNode = CreateInfixExpressionNode(actualToken);
                    if (expressionNode == null)
                    {
                        // syntax error
                        ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                    }
                    multiplicationInFixExpressionNode.LeftSideNode = expressionNode;

                    _logger.LogInformation("=== created node ===");
                    _logger.LogInformation("== node type: {NodeType}", multiplicationInFixExpressionNode.GetType());
                    _logger.LogInformation("== node.LeftSideNode type: {NodeType}",
                                           multiplicationInFixExpressionNode?.LeftSideNode?.GetType());
                    _logger.LogInformation("== node.RightSideNode type: {NodeType}",
                                           multiplicationInFixExpressionNode?.RightSideNode?.GetType());
                    _logger.LogInformation("== previous precedence: {P}", precedence);
                    _logger.LogInformation("== token precedence: {P}", actualTokenPrecedence);
                    _logger.LogInformation("\n \n");
                    return BuildExpressionTree(tokens, position + 1, multiplicationInFixExpressionNode,
                                               actualTokenPrecedence);

                case TokenTypes.Sign_Division:
                    InFixExpressionNode divisionInFixExpressionNode = CreateInfixExpressionNode(actualToken);
                    if (expressionNode == null)
                    {
                        // syntax error
                        ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                    }
                    divisionInFixExpressionNode.LeftSideNode = expressionNode;

                    _logger.LogInformation("=== created node ===");
                    _logger.LogInformation("== node type: {NodeType}", divisionInFixExpressionNode.GetType());
                    _logger.LogInformation("== node.LeftSideNode type: {NodeType}",
                                           divisionInFixExpressionNode?.LeftSideNode?.GetType());
                    _logger.LogInformation("== node.RightSideNode type: {NodeType}",
                                           divisionInFixExpressionNode?.RightSideNode?.GetType());
                    _logger.LogInformation("== previous precedence: {P}", precedence);
                    _logger.LogInformation("== token precedence: {P}", actualTokenPrecedence);
                    _logger.LogInformation("\n \n");
                    return BuildExpressionTree(tokens, position + 1, divisionInFixExpressionNode,
                                               actualTokenPrecedence);
            }
        }

        switch (actualToken.TokenType)
        {
            case TokenTypes.IntegerLiteral:
                IntegerLiteralExpressionNode integerLiteralExpressionNode =
                    CreateIntegerLiteralExpressionNode(actualToken);
                IExpressionNode nodePassedFurther = null;
                if (expressionNode == null)
                {
                    nodePassedFurther = integerLiteralExpressionNode;
                }
                if (expressionNode != null && expressionNode.GetType() == typeof(InFixExpressionNode))
                {
                    InFixExpressionNode parentNode = (InFixExpressionNode)expressionNode;
                    // sticky to the left
                    if (parentNode.LeftSideNode == null)
                    {
                        parentNode.LeftSideNode = integerLiteralExpressionNode;
                    }
                    else
                    {
                        // if the left is not free then to the right
                        parentNode.RightSideNode = integerLiteralExpressionNode;
                    }
                    nodePassedFurther = parentNode;
                }
                _logger.LogInformation("=== created node ===");
                _logger.LogInformation("=== node type: {NodeType}", nodePassedFurther?.GetType());
                _logger.LogInformation("=== previous precedence: {Precedence}", precedence);
                _logger.LogInformation("=== token precedence: {Precedence}", actualTokenPrecedence);
                _logger.LogInformation("\n \n");
                return BuildExpressionTree(tokens, position + 1, nodePassedFurther, actualTokenPrecedence);

            case TokenTypes.Sign_Semicolon:
                _logger.LogInformation("=== RETURN \n \n");
                return (expressionNode, position + 1);

            default:
                throw new PotatoParserException();
        }

    }

    private ExpressionPrecedences DeterminePrecedence(PotatoToken token)
    {
        _logger.LogInformation($"token precedence: {token.TokenType}");
        return token.TokenType switch {
            TokenTypes.IntegerLiteral => ExpressionPrecedences.Literals,
            TokenTypes.Sign_Semicolon => ExpressionPrecedences.Literals,
            TokenTypes.Sign_Addition => ExpressionPrecedences.AdditionAndSubtraction,
            TokenTypes.Sign_Subtraction => ExpressionPrecedences.AdditionAndSubtraction,
            TokenTypes.Sign_Multiplication => ExpressionPrecedences.MultiplicationAndDivision,
            TokenTypes.Sign_Division => ExpressionPrecedences.MultiplicationAndDivision,
            _ => throw new PotatoParserException("No such precedence"),
        };
    }

    private InFixExpressionNode CreateInfixExpressionNode(PotatoToken token) =>
        new() {
            TokenTypeStringLiteral = token.TokenType,
        };

    private IntegerLiteralExpressionNode CreateIntegerLiteralExpressionNode(PotatoToken actualToken) =>
        new() {
            Value = int.Parse(actualToken.Value),
            StringTokenLiteral = actualToken.TokenType,
            StringValueLiteral = actualToken.Value,
        };

    private enum ExpressionPrecedences
    {
        Lowest,
        Literals,
        AdditionAndSubtraction,
        MultiplicationAndDivision,
    }
}
