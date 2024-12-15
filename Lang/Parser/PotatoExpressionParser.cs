namespace Potato.Parser;

using AstNodes;

/// <summary>
///     The Potato Expression Parser.
///     It parses the Expression type AST nodes.
///     <example>
///         <code>
/// expression      -> equality ;
/// equality        -> comparison ( ( "!=" | "==" ) comparison ) * ;
/// comparison      -> term ( ( ">" | ">=" | "<" | "<=" ) term ) * ;
/// term            -> factor ( ( "-" | "+" ) factor ) * ;
/// factor          -> unary ( ( "/" | "*" ) unary ) * ;
/// unary           -> ( "!" | "-" ) unary | primary ;
/// primary         -> NUMBER | STRING | "true" | "false" | "null" | "(" expression ")" ;
/// ---
/// equality examples:
/// --- no variables
/// 111 == 222
/// ("asd" == "bds") != "asdfasd"
///
/// --- variable version
/// somethingA == somethingB;
/// (somethingA == somethingB) != somethingC;
/// (somethingA == somethingB) != (somethingC == somethingD);
/// </code>
///     </example>
/// </summary>
public class PotatoExpressionParser
{
    public (IPotatoAstNode ExpressionNodes, int ContinuationPosition) ParseExpressions(
        List<PotatoToken> tokens, int position)
    {
        PotatoRootAstNode nodes = new();
        (IPotatoAstNode EqualityExpressionNode, int ContinuationAfterEquality) equalityExpressions =
            ParseEqualityExpression(tokens, position);
        return equalityExpressions;
    }

    private (IEqualityExpressionAstNode Node, int Continuation) ParseEqualityExpression(
        List<PotatoToken> tokens,
        int position)
    {
        List<string> targets = new() {
            TokenTypes.Sign_DoubleEquality,
            TokenTypes.Sign_BangEquality,
        };
        int targetPosition = ParserHelpers.Find(targets, tokens, position);

        if (!ParserHelpers.CheckPositionExists(tokens, targetPosition - 1)
         || !ParserHelpers.CheckPositionExists(tokens, targetPosition + 1))
        {
            // erroring out
        }

        PotatoToken leftHandSideToken = tokens[targetPosition - 1];
        PotatoToken rightHandSideToken = tokens[targetPosition + 1];

        if (leftHandSideToken.TokenType != rightHandSideToken.TokenType
            // to protect the code from the identifier cases
         || leftHandSideToken.TokenType == TokenTypesEnum.Identifier &&
            rightHandSideToken.TokenType == TokenTypesEnum.Identifier)
        {
            // erroring out
        }

        return (
            ParserHelpers.CreateEqualityExpressionNode(leftHandSideToken, rightHandSideToken),
            targetPosition + 1
        );

    }
}
