namespace Potato.Parser;

using AstNodes;

using Microsoft.Extensions.Logging;

public partial class Parser
{
    private readonly Stack<int> findGroupedExpressionsStack = new();

    private IExpressionNode ParseExpressions(
        List<PotatoToken> tokens
    ) => BuildExpressionTree(0, null, tokens, 0);

    /// <summary>
    ///     Builds the Abstract Syntax Tree using the expression in the variable assignment.
    ///     The code receives only the variable assignment left side, between the = and ; signs.
    /// </summary>
    /// <param name="actualPosition">The index of the element where the processing takes place.</param>
    /// <param name="abstractSyntaxTree">
    ///     The abstract syntax tree which will be extended and modified by every recursion.
    /// </param>
    /// <param name="originalTokens">The original list of tokens. It represents the source code to the fullest.</param>
    /// <param name="groupedScopeDepth">
    ///     It represents the grouped expressions scope. The deeper the builder in the nested group expressions (things put
    ///     into parantheses to manipulate precedences) the higher this number will be.
    ///     Once the grouped expression processing ends this number will be reduced by one. This is kind of backtracking
    ///     thingy.
    /// </param>
    /// <returns></returns>
    /// <exception cref="PotatoParserException"></exception>
    private IExpressionNode BuildExpressionTree(
        int actualPosition,
        IExpressionNode? abstractSyntaxTree,
        List<PotatoToken> originalTokens,
        int groupedScopeDepth)
    {
        int position = SkipTokensInExpressionParsing(originalTokens, actualPosition);
        PotatoToken actualToken = GetToken(originalTokens, position);
        int positionForPeekToken = SkipTokensInExpressionParsing(originalTokens,
                                                                 SkipTokensInExpressionParsing(
                                                                     originalTokens, actualPosition + 1));
        PotatoToken peekToken = GetToken(originalTokens, positionForPeekToken);
        IExpressionNode? continuationNode = FindContinuationNode(abstractSyntaxTree);

        switch (actualToken.TokenType)
        {
            case TokenTypesEnum.Sign_Addition:
                InFixExpressionNode additionInFixExpressionNode = CreateInfixExpressionNode(actualToken,
                    groupedScopeDepth);
                if (continuationNode == null)
                {
                    // syntax error
                    ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                }
                // if the continuation node is value node
                // and it doesn't have a parent (meaning it is not part of an expression)
                if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                 && continuationNode.ParentExpressionNode == null)
                {
                    continuationNode.IsContinuationPosition = false;
                    additionInFixExpressionNode.LeftSideNode = continuationNode;
                    abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                             additionInFixExpressionNode,
                                                             originalTokens,
                                                             groupedScopeDepth);
                }

                // if the continuation node is value node
                // and it does have a parent (meaning it is part of an expression)
                // and the parent precedence lower or equal to what we create now
                // then the PULL LEFT rule applies
                if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                 && continuationNode.ParentExpressionNode != null
                 && (DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType) ==
                     DeterminePrecedence(additionInFixExpressionNode.TokenType)
                  || DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType) <=
                     DeterminePrecedence(additionInFixExpressionNode.TokenType)))
                {
                    continuationNode.IsContinuationPosition = false;
                    additionInFixExpressionNode.LeftSideNode = continuationNode.ParentExpressionNode;
                    abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                             additionInFixExpressionNode,
                                                             originalTokens,
                                                             groupedScopeDepth);
                }
                break;


            case TokenTypesEnum.Sign_Subtraction:
                InFixExpressionNode subtractionInFixExpressionNode = CreateInfixExpressionNode(actualToken,
                    groupedScopeDepth);
                if (continuationNode == null)
                {
                    // syntax error
                    ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                }
                // if the continuation node is value node
                // and it doesn't have a parent (meaning it is not part of an expression)
                if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                 && continuationNode.ParentExpressionNode == null)
                {
                    continuationNode.IsContinuationPosition = false;
                    subtractionInFixExpressionNode.LeftSideNode = continuationNode;
                    abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                             subtractionInFixExpressionNode,
                                                             originalTokens,
                                                             groupedScopeDepth);
                }

                // if the continuation node is value node
                // and it does have a parent (meaning it is part of an expression)
                // and the parent precedence lower or equal to what we create now
                // then the PULL LEFT rule applies
                if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                 && continuationNode.ParentExpressionNode != null
                 && (DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType) ==
                     DeterminePrecedence(subtractionInFixExpressionNode.TokenType)
                  || DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType) <=
                     DeterminePrecedence(subtractionInFixExpressionNode.TokenType)))
                {
                    continuationNode.IsContinuationPosition = false;
                    subtractionInFixExpressionNode.LeftSideNode = continuationNode.ParentExpressionNode;
                    abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                             subtractionInFixExpressionNode,
                                                             originalTokens,
                                                             groupedScopeDepth);
                }
                break;

            case TokenTypesEnum.Sign_Multiplication:
                InFixExpressionNode multiplicationInFixExpressionNode = CreateInfixExpressionNode(actualToken,
                    groupedScopeDepth);
                if (continuationNode == null)
                {
                    // syntax error
                    ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                }
                continuationNode!.IsContinuationPosition = false;

                // the cases when the character AFTER the divison sign is not parentheses.
                // parentheses means change in precedence
                if (peekToken.TokenType != TokenTypesEnum.Sign_OpenParentheses)
                {
                    // if the continuation node
                    // is literal node
                    // and not grouped
                    // and doesn't have parent (meaning not part of grouping)
                    if ((continuationNode.GetType() == typeof(IntegerLiteralExpressionNode)
                      || continuationNode.GetType() == typeof(StringLiteralExpressionNode))
                     && continuationNode.Group == 0)
                    {
                        // if the continuation node has
                        // no parent
                        // and the precedence of left expression is less
                        // then we steel its RIGHT value and we put ourselves there
                        // example: 1 - 2 / 3
                        if (continuationNode.ParentExpressionNode != null
                         && DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType)
                          < DeterminePrecedence(multiplicationInFixExpressionNode.TokenType))
                        {
                            IExpressionNode parent = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                            InFixExpressionNode parentTyped = (InFixExpressionNode)parent;
                            multiplicationInFixExpressionNode.LeftSideNode = parentTyped.RightSideNode;
                            parentTyped.RightSideNode = multiplicationInFixExpressionNode;
                            abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                     parent,
                                                                     originalTokens,
                                                                     groupedScopeDepth);
                        }

                        if (continuationNode.ParentExpressionNode != null
                         && DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType)
                         == DeterminePrecedence(multiplicationInFixExpressionNode.TokenType))
                        {
                            IExpressionNode parent = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                            InFixExpressionNode parentTyped = (InFixExpressionNode)parent;
                            multiplicationInFixExpressionNode.LeftSideNode = parentTyped;
                            abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                     multiplicationInFixExpressionNode,
                                                                     originalTokens,
                                                                     groupedScopeDepth);
                        }

                        // example: 1 / 2
                        if (continuationNode.ParentExpressionNode == null)
                        {
                            multiplicationInFixExpressionNode.LeftSideNode = continuationNode;
                            abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                     multiplicationInFixExpressionNode,
                                                                     originalTokens,
                                                                     groupedScopeDepth);
                        }

                    }
                    // if the continuation node
                    // is literal
                    // and part of a grouped expression
                    // and has SMALLER precedence than actual token
                    // then its precedence is stronger and gets to the LEFT side
                    if ((continuationNode.GetType() == typeof(IntegerLiteralExpressionNode)
                      || continuationNode.GetType() == typeof(StringLiteralExpressionNode))
                     && continuationNode.ParentExpressionNode != null
                     && continuationNode.Group > 0)
                    {
                        IExpressionNode parent = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                        multiplicationInFixExpressionNode.LeftSideNode = parent;
                        abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                 multiplicationInFixExpressionNode,
                                                                 originalTokens,
                                                                 groupedScopeDepth);
                    }
                }
                else
                {
                    // the cases where the next character after the division sign is open parentheses, meaning
                    // there is a change in precedence and we have to deal with it
                    if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                     && continuationNode.ParentExpressionNode != null
                     && continuationNode.Group > 0)
                    {
                        IExpressionNode topNode = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                        multiplicationInFixExpressionNode.LeftSideNode = topNode;
                        multiplicationInFixExpressionNode.RightSideNode = BuildExpressionTree(position + 1,
                            null,
                            originalTokens,
                            groupedScopeDepth);
                        abstractSyntaxTree = multiplicationInFixExpressionNode;
                    }

                    if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                     && continuationNode.ParentExpressionNode != null
                     && continuationNode.Group == 0)
                    {
                        IExpressionNode topNode = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                        if (DeterminePrecedence(topNode.TokenType) ==
                            DeterminePrecedence(multiplicationInFixExpressionNode.TokenType))
                        {
                            multiplicationInFixExpressionNode.LeftSideNode = topNode;
                            multiplicationInFixExpressionNode.RightSideNode = BuildExpressionTree(position + 1,
                                null,
                                originalTokens,
                                groupedScopeDepth);
                            abstractSyntaxTree = multiplicationInFixExpressionNode;
                        }
                    }
                }
                break;


            case TokenTypesEnum.Sign_Division:
                InFixExpressionNode divisionInFixExpressionNode = CreateInfixExpressionNode(actualToken,
                    groupedScopeDepth);
                if (continuationNode == null)
                {
                    // syntax error
                    ParserHelpers.ThrowParseException("syntax error", actualToken.LineNumber, position);
                }
                continuationNode!.IsContinuationPosition = false;

                // the cases when the character AFTER the divison sign is not parentheses.
                // parentheses means change in precedence
                if (peekToken.TokenType != TokenTypesEnum.Sign_OpenParentheses)
                {
                    // if the continuation node
                    // is literal node
                    // and not grouped
                    // and doesn't have parent (meaning not part of grouping)
                    if ((continuationNode.GetType() == typeof(IntegerLiteralExpressionNode)
                      || continuationNode.GetType() == typeof(StringLiteralExpressionNode))
                     && continuationNode.Group == 0)
                    {
                        // if the continuation node has
                        // no parent
                        // and the precedence of left expression is less
                        // then we steel its RIGHT value and we put ourselves there
                        // example: 1 - 2 / 3
                        if (continuationNode.ParentExpressionNode != null
                         && DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType)
                          < DeterminePrecedence(divisionInFixExpressionNode.TokenType))
                        {
                            IExpressionNode parent = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                            InFixExpressionNode parentTyped = (InFixExpressionNode)parent;
                            divisionInFixExpressionNode.LeftSideNode = parentTyped.RightSideNode;
                            parentTyped.RightSideNode = divisionInFixExpressionNode;
                            abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                     parent,
                                                                     originalTokens,
                                                                     groupedScopeDepth);
                        }

                        if (continuationNode.ParentExpressionNode != null
                         && DeterminePrecedence(continuationNode.ParentExpressionNode.TokenType)
                         == DeterminePrecedence(divisionInFixExpressionNode.TokenType))
                        {
                            IExpressionNode parent = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                            InFixExpressionNode parentTyped = (InFixExpressionNode)parent;
                            divisionInFixExpressionNode.LeftSideNode = parentTyped;
                            abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                     divisionInFixExpressionNode,
                                                                     originalTokens,
                                                                     groupedScopeDepth);
                        }

                        // example: 1 / 2
                        if (continuationNode.ParentExpressionNode == null)
                        {
                            divisionInFixExpressionNode.LeftSideNode = continuationNode;
                            abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                     divisionInFixExpressionNode,
                                                                     originalTokens,
                                                                     groupedScopeDepth);
                        }

                    }
                    // if the continuation node
                    // is literal
                    // and part of a grouped expression
                    // and has SMALLER precedence than actual token
                    // then its precedence is stronger and gets to the LEFT side
                    if ((continuationNode.GetType() == typeof(IntegerLiteralExpressionNode)
                      || continuationNode.GetType() == typeof(StringLiteralExpressionNode))
                     && continuationNode.ParentExpressionNode != null
                     && continuationNode.Group > 0)
                    {
                        IExpressionNode parent = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                        divisionInFixExpressionNode.LeftSideNode = parent;
                        abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                 divisionInFixExpressionNode,
                                                                 originalTokens,
                                                                 groupedScopeDepth);
                    }
                }
                else
                {
                    // the cases where the next character after the division sign is open parentheses, meaning
                    // there is a change in precedence and we have to deal with it
                    if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                     && continuationNode.ParentExpressionNode != null
                     && continuationNode.Group > 0)
                    {
                        IExpressionNode topNode = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                        divisionInFixExpressionNode.LeftSideNode = topNode;
                        divisionInFixExpressionNode.RightSideNode = BuildExpressionTree(position + 1,
                            null,
                            originalTokens,
                            groupedScopeDepth);
                        abstractSyntaxTree = divisionInFixExpressionNode;
                    }

                    if (continuationNode?.GetType() == typeof(IntegerLiteralExpressionNode)
                     && continuationNode.ParentExpressionNode != null
                     && continuationNode.Group == 0)
                    {
                        IExpressionNode topNode = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                        if (DeterminePrecedence(topNode.TokenType) ==
                            DeterminePrecedence(divisionInFixExpressionNode.TokenType))
                        {
                            divisionInFixExpressionNode.LeftSideNode = topNode;
                            divisionInFixExpressionNode.RightSideNode = BuildExpressionTree(position + 1,
                                null,
                                originalTokens,
                                groupedScopeDepth);
                            abstractSyntaxTree = divisionInFixExpressionNode;
                        }
                    }
                }
                break;


            case TokenTypesEnum.StringLiteral:
                _logger.LogInformation($"There is an uncovered case in {nameof(TokenTypes.StringLiteral)}");
                break;

            case TokenTypesEnum.IntegerLiteral:
                IntegerLiteralExpressionNode integerLiteralExpressionNode =
                    CreateIntegerLiteralExpressionNode(actualToken, groupedScopeDepth);

                if (continuationNode?.GetType() == typeof(ILiteralValueNode)
                 && continuationNode.Group == 0)
                {
                    throw new PotatoParserException(
                        "Syntax error! An integer literal follows another integer literal which is not allowed."
                    );
                }

                // when the actual token is the first in a series of expressions there is no expression before it
                if (continuationNode == null)
                {
                    abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                             integerLiteralExpressionNode,
                                                             originalTokens,
                                                             groupedScopeDepth);
                }
                else
                {
                    // we need to find the top of the grouping
                    if (continuationNode!.GetType() == typeof(ILiteralValueNode)
                     && continuationNode.Group > 0)
                    {
                        continuationNode = FindTheTopOfTheGroup(continuationNode, continuationNode.Group);
                    }

                    // when there is an expression node (infixexpression node) before the integer character
                    if (continuationNode.GetType() == typeof(InFixExpressionNode))
                    {
                        InFixExpressionNode infixTypedContinuationNode = (InFixExpressionNode)continuationNode;
                        infixTypedContinuationNode.IsContinuationPosition = false;
                        // I don't know when this happens...
                        if (infixTypedContinuationNode.LeftSideNode == null)
                        {
                            infixTypedContinuationNode.LeftSideNode = integerLiteralExpressionNode;
                            integerLiteralExpressionNode.ParentExpressionNode = infixTypedContinuationNode;
                        }
                        else
                        {
                            infixTypedContinuationNode.RightSideNode = integerLiteralExpressionNode;
                            integerLiteralExpressionNode.ParentExpressionNode = infixTypedContinuationNode;
                        }
                        abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                                 abstractSyntaxTree,
                                                                 originalTokens,
                                                                 groupedScopeDepth);

                    }
                }
                break;


            case TokenTypesEnum.Sign_OpenParentheses:
                // if the stack, which contains the grouped expressions ranges, is empty
                // we are going to discover these groupes and put their ranges in the stack
                Range groupedExpressionRange = FindGroupedExpressionInProvidedTokens(originalTokens, position);

                if (groupedExpressionRange.Start.Value != position)
                {
                    throw new PotatoParserException(
                        $"Grouped expression start, {groupedExpressionRange.Start.Value}, " +
                        $"is not equal to position, {position}");
                }
                abstractSyntaxTree = BuildExpressionTree(groupedExpressionRange.Start.Value + 1,
                                                         null,
                                                         originalTokens,
                                                         groupedScopeDepth + 1);
                break;

            case TokenTypesEnum.Sign_CloseParentheses:
                int groupedScopeDepthNew = groupedScopeDepth;
                if (groupedScopeDepth > 0)
                {
                    groupedScopeDepthNew = groupedScopeDepth - 1;
                }

                // we switch back one level of grouping, it means
                // we pass the original tokens as "worktokens", so the processing can continue
                // we reduce the grouping id
                abstractSyntaxTree = BuildExpressionTree(position + 1,
                                                         abstractSyntaxTree,
                                                         originalTokens,
                                                         groupedScopeDepthNew);
                break;

            case TokenTypesEnum.Sign_Semicolon:
                return abstractSyntaxTree;

            default:
                string msg = $"There is no such type in the switch like {actualToken.TokenType}";
                throw new PotatoParserException(msg);
        }

        return abstractSyntaxTree;
    }

    private IExpressionNode FindTheTopOfTheGroup(IExpressionNode node, int groupId)
    {
        if (node.ParentExpressionNode != null && node.ParentExpressionNode.Group == groupId)
        {
            return FindTheTopOfTheGroup(node.ParentExpressionNode, groupId);
        }
        return node;
    }

    public IExpressionNode? FindContinuationNode(IExpressionNode? abstractSyntaxTree)
    {
        if (abstractSyntaxTree != null)
        {
            if (abstractSyntaxTree.GetType() == typeof(StringLiteralExpressionNode))
            {
                StringLiteralExpressionNode stringLiteralExpressionNode =
                    (StringLiteralExpressionNode)abstractSyntaxTree;
                if (stringLiteralExpressionNode.IsContinuationPosition)
                {
                    return stringLiteralExpressionNode;
                }
            }

            if (abstractSyntaxTree.GetType() == typeof(IntegerLiteralExpressionNode))
            {
                IntegerLiteralExpressionNode integerLiteralExpressionNode =
                    (IntegerLiteralExpressionNode)abstractSyntaxTree;
                if (integerLiteralExpressionNode.IsContinuationPosition)
                {
                    return integerLiteralExpressionNode;
                }
            }

            if (abstractSyntaxTree.GetType() == typeof(InFixExpressionNode))
            {
                InFixExpressionNode inFixExpressionNode = (InFixExpressionNode)abstractSyntaxTree;
                if (inFixExpressionNode.IsContinuationPosition)
                {
                    return inFixExpressionNode;
                }
                IExpressionNode? result1 = FindContinuationNode(inFixExpressionNode.LeftSideNode);
                if (result1 != null)
                {
                    return result1;
                }
                IExpressionNode? result2 = FindContinuationNode(inFixExpressionNode.RightSideNode);
                if (result2 != null)
                {
                    return result2;
                }
            }
        }
        return null;
    }

    private PotatoToken GetToken(List<PotatoToken> tokens, int position)
    {
        if (position >= tokens.Count)
        {
            return tokens[^1];
        }
        return tokens[position];
    }

    private Range FindGroupedExpressionInProvidedTokens(List<PotatoToken> tokens, int startPositionOfExpression)
    {
        findGroupedExpressionsStack.Clear();
        for (int i = startPositionOfExpression; i < tokens.Count; i++)
        {
            if (tokens[i].TokenType == TokenTypesEnum.Sign_OpenParentheses)
            {
                findGroupedExpressionsStack.Push(i);
            }
            if (tokens[i].TokenType == TokenTypesEnum.Sign_CloseParentheses)
            {
                int startOfTheNestedTokens = findGroupedExpressionsStack.Pop();
                if (!findGroupedExpressionsStack.Any())
                {
                    return new Range(startOfTheNestedTokens, i);
                }
            }
        }
        throw new PotatoParserException(
            "Possible syntax error!");
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
        if (i >= tokens.Count)
        {
            return i;
        }
        PotatoToken actualToken = tokens[i];
        switch (actualToken.TokenType)
        {
            case TokenTypesEnum.Sign_DoubleQuote:
                _logger.LogInformation("\n \n == Token type: {TokenType} skipped \n \n", TokenTypes.Sign_DoubleQuote);
                return i + 1;
        }
        return i;
    }


    private ExpressionPrecedences DeterminePrecedence(PotatoToken token) => DeterminePrecedence(token.TokenType);

    private ExpressionPrecedences DeterminePrecedence(TokenTypesEnum tokenType)
    {
        return tokenType switch {
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

            _ => throw new PotatoParserException($"No such precedence like {tokenType}."),
        };

    }

    private InFixExpressionNode CreateInfixExpressionNode(
        PotatoToken token,
        int groupedExpressionDepth) =>
        new() {
            TokenType = token.TokenType,
            Group = groupedExpressionDepth,
            IsContinuationPosition = true,
        };

    private StringLiteralExpressionNode CreateStringLiteralExpressionNode(
        PotatoToken actualToken,
        int groupedExpressionDepth) =>
        new() {
            Value = actualToken.Value,
            TokenType = TokenTypesEnum.StringLiteral,
            ValueLiteral = actualToken.Value,
            Group = groupedExpressionDepth,
        };

    private IntegerLiteralExpressionNode CreateIntegerLiteralExpressionNode(
        PotatoToken actualToken,
        int groupedExpressionDepth) =>
        new() {
            Value = int.Parse(actualToken.Value),
            TokenType = TokenTypesEnum.IntegerLiteral,
            ValueLiteral = actualToken.Value,
            Group = groupedExpressionDepth,
            IsContinuationPosition = true,
        };

    private enum ExpressionPrecedences
    {
        Lowest,
        Literals,
        AdditionAndSubtraction,
        MultiplicationAndDivision,
    }
}
