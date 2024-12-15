namespace Potato.Parser;

using AstNodes;

using Microsoft.Extensions.Logging;

public partial class Parser
{
    private (IExpressionNode AssignmentExpressionNode, int ContinuationPosition)
        ParseExpressions(
            List<PotatoToken> tokens
        ) => BuildExpressionTree(tokens, 0, null, ExpressionPrecedences.Lowest, 0, tokens);

    private (IExpressionNode ExpressionNode, int ContinuationPosition) BuildExpressionTree(
        List<PotatoToken> workTokens,
        int inputPosition,
        IExpressionNode? expressionNode,
        ExpressionPrecedences precedence,
        int endPositionOfExpression,
        List<PotatoToken> originalTokens)
    {

        _logger.LogInformation("==== New token ====");
        _logger.LogInformation("=== Input ===");
        _logger.LogInformation("== Token type: {Token}", workTokens[inputPosition].TokenType);
        _logger.LogInformation("== Token literal: {Token}", workTokens[inputPosition].Value);
        _logger.LogInformation("== Received node type: {NodeType}", expressionNode?.GetType());
        _logger.LogInformation("== Received precedence: {NodeType}", precedence);


        int position = SkipTokensInExpressionParsing(workTokens, inputPosition);
        PotatoToken actualToken = workTokens[position];
        ExpressionPrecedences actualTokenPrecedence = DeterminePrecedence(actualToken);

        if (actualTokenPrecedence > precedence && actualToken.TokenType != TokenTypesEnum.Sign_Semicolon)
        {
            // higher precedence means that a new parent node will be created
            // create a new expression node which will be the parent of the existing node
            // add existing node to left
            switch (actualToken.TokenType)
            {
                case TokenTypesEnum.Sign_Addition:
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
                    return BuildExpressionTree(workTokens,
                                               position + 1,
                                               additionInFixExpressionNode,
                                               actualTokenPrecedence,
                                               endPositionOfExpression,
                                               originalTokens);

                case TokenTypesEnum.Sign_Subtraction:
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
                    return BuildExpressionTree(workTokens,
                                               position + 1,
                                               subtractionInFixExpressionNode,
                                               actualTokenPrecedence,
                                               endPositionOfExpression,
                                               originalTokens);

                case TokenTypesEnum.Sign_Multiplication:
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
                    return BuildExpressionTree(workTokens,
                                               position + 1,
                                               multiplicationInFixExpressionNode,
                                               actualTokenPrecedence,
                                               endPositionOfExpression,
                                               originalTokens);

                case TokenTypesEnum.Sign_Division:
                    InFixExpressionNode divisionInFixExpressionNode = CreateInfixExpressionNode(actualToken);
                    if (expressionNode == null)
                    {
                        // syntax error
                        ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                    }
                    if (expressionNode?.GetType() == typeof(InFixExpressionNode))
                    {
                        InFixExpressionNode n = (InFixExpressionNode)expressionNode;
                        DeterminePrecedence(n.TokenType)
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
                    return BuildExpressionTree(workTokens,
                                               position + 1,
                                               divisionInFixExpressionNode,
                                               actualTokenPrecedence,
                                               endPositionOfExpression,
                                               originalTokens);
            }
        }

        switch (actualToken.TokenType)
        {
            case TokenTypesEnum.StringLiteral:
                StringLiteralExpressionNode stringLiteralExpressionNode =
                    CreateStringLiteralExpressionNode(actualToken);
                IExpressionNode stringNodePassedFurther = null;
                if (expressionNode == null)
                {
                    stringNodePassedFurther = stringLiteralExpressionNode;
                }
                if (expressionNode != null && expressionNode.GetType() == typeof(InFixExpressionNode))
                {
                    InFixExpressionNode parentNode = (InFixExpressionNode)expressionNode;
                    // sticky to the left
                    if (parentNode.LeftSideNode == null)
                    {
                        parentNode.LeftSideNode = stringLiteralExpressionNode;
                    }
                    else
                    {
                        // if the left is not free then to the right
                        parentNode.RightSideNode = stringLiteralExpressionNode;
                    }
                    stringNodePassedFurther = parentNode;
                }
                _logger.LogInformation("=== created node ===");
                _logger.LogInformation("=== node type: {NodeType}", stringNodePassedFurther?.GetType());
                _logger.LogInformation("=== previous precedence: {Precedence}", precedence);
                _logger.LogInformation("=== token precedence: {Precedence}", actualTokenPrecedence);
                _logger.LogInformation("\n \n");
                return BuildExpressionTree(workTokens,
                                           position + 1,
                                           stringNodePassedFurther,
                                           actualTokenPrecedence,
                                           endPositionOfExpression,
                                           originalTokens);

            case TokenTypesEnum.IntegerLiteral:
                IntegerLiteralExpressionNode integerLiteralExpressionNode =
                    CreateIntegerLiteralExpressionNode(actualToken);
                IExpressionNode integerNodePassedFurther = null;
                if (expressionNode == null)
                {
                    integerNodePassedFurther = integerLiteralExpressionNode;
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
                        // I don't know when this happens
                        parentNode.RightSideNode = integerLiteralExpressionNode;
                    }
                    integerNodePassedFurther = parentNode;
                }
                _logger.LogInformation("=== created node ===");
                _logger.LogInformation("=== node type: {NodeType}", integerNodePassedFurther?.GetType());
                _logger.LogInformation("=== previous precedence: {Precedence}", precedence);
                _logger.LogInformation("=== token precedence: {Precedence}", actualTokenPrecedence);
                _logger.LogInformation("\n \n");
                return BuildExpressionTree(workTokens,
                                           position + 1,
                                           integerNodePassedFurther,
                                           actualTokenPrecedence,
                                           endPositionOfExpression,
                                           originalTokens);

            case TokenTypesEnum.Sign_OpenParentheses:
                (List<PotatoToken> ExpressionTokens, int EndPositionOfExpression) expressionTokens =
                    FindGroupedExpressionInProvidedTokens(workTokens, position);
                return BuildExpressionTree(expressionTokens.ExpressionTokens,
                                           0,
                                           null,
                                           ExpressionPrecedences.Lowest,
                                           position + expressionTokens.EndPositionOfExpression,
                                           originalTokens);

            case TokenTypesEnum.Sign_CloseParentheses:
                return BuildExpressionTree(originalTokens,
                                           endPositionOfExpression,
                                           expressionNode,
                                           ExpressionPrecedences.Lowest,
                                           endPositionOfExpression,
                                           originalTokens);

            case TokenTypesEnum.Sign_Semicolon:
                _logger.LogInformation("=== RETURN \n \n");
                return (expressionNode, position + 1);

            default:
                string msg = $"There is no such type in the switch like {actualToken.TokenType}";
                throw new PotatoParserException(msg);
        }

    }


    private (List<PotatoToken> GroupedExpressionTokens, int LastPosition)
        FindGroupedExpressionInProvidedTokens(List<PotatoToken> tokens, int position)
    {
        Stack<int> stack = new();
        Range range = default;
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].TokenType == TokenTypesEnum.Sign_OpenParentheses)
            {
                stack.Push(i);
            }
            if (tokens[i].TokenType == TokenTypesEnum.Sign_CloseParentheses)
            {
                int startOfTheNestedTokens = stack.Pop() + 1;
                if (!stack.Any())
                {
                    range = new Range(startOfTheNestedTokens, i + 1);
                }
            }
        }
        if (range.Equals(default))
        {
            string msg = $"The parentheses in line {tokens[position].LineNumber} and at position {position} " +
                         $"doesn't have a closing parentheses. " +
                         $"Line: {string.Join("", tokens)}";
            throw new PotatoParserException(msg);
        }
        return (
            tokens.Take(range).ToList(),
            range.End.Value
        );
    }

    /// <summary>
    ///     There are <see cref="TokenTypes" /> we need to skip in parsing variable expressions.
    ///     For example, <see cref="TokenTypes.Sign_DoubleQuote" /> needs to be skipped.
    /// </summary>
    /// <param name="tokens">The list of tokens.</param>
    /// <param name="i">The position where the processing stands in the list of tokens.</param>
    /// <returns>The continuation position of the processing.</returns>
    private int SkipTokensInExpressionParsing(List<PotatoToken> tokens, int i)
    {
        PotatoToken actualToken = tokens[i];
        switch (actualToken.TokenType)
        {
            case TokenTypesEnum.Sign_DoubleQuote:
                _logger.LogInformation("\n \n == Token type: {TokenType} skipped \n \n", TokenTypes.Sign_DoubleQuote);
                return i + 1;
        }
        return i;
    }


    private ExpressionPrecedences DeterminePrecedence(PotatoToken token)
    {
        return token.TokenType switch {
            TokenTypesEnum.Sign_OpenParentheses => ExpressionPrecedences.Lowest,
            TokenTypesEnum.Sign_CloseParentheses => ExpressionPrecedences.Lowest,

            TokenTypesEnum.IntegerLiteral => ExpressionPrecedences.Literals,
            TokenTypesEnum.StringLiteral => ExpressionPrecedences.Literals,
            TokenTypesEnum.Sign_Semicolon => ExpressionPrecedences.Literals,
            TokenTypesEnum.Sign_DoubleQuote => ExpressionPrecedences.Literals,

            TokenTypesEnum.Sign_Addition => ExpressionPrecedences.AdditionAndSubtraction,
            TokenTypesEnum.Sign_Subtraction => ExpressionPrecedences.AdditionAndSubtraction,

            TokenTypesEnum.Sign_Multiplication => ExpressionPrecedences.MultiplicationAndDivision,
            TokenTypesEnum.Sign_Division => ExpressionPrecedences.MultiplicationAndDivision,

            _ => throw new PotatoParserException($"No such precedence like {token.TokenType}."),
        };
    }

    private InFixExpressionNode CreateInfixExpressionNode(PotatoToken token) =>
        new() {
            TokenType = token.TokenType,
        };

    private StringLiteralExpressionNode CreateStringLiteralExpressionNode(PotatoToken actualToken) =>
        new() {
            Value = actualToken.Value,
            StringTokenLiteral = actualToken.TokenStringLiteral,
            StringValueLiteral = actualToken.Value,
        };

    private IntegerLiteralExpressionNode CreateIntegerLiteralExpressionNode(PotatoToken actualToken) =>
        new() {
            Value = int.Parse(actualToken.Value),
            StringTokenLiteral = actualToken.TokenStringLiteral,
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
