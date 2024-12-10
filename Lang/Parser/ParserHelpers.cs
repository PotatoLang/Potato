namespace Potato.Parser;

using AstNodes;

public static class ParserHelpers
{
    public static int Find(List<string> targets, List<PotatoToken> tokens, int position)
    {
        for (int i = position; i < tokens.Count; i++)
        {
            if (targets.Any(target => target == tokens[i].TokenType))
            {
                return i;
            }
        }
        return 0;
    }

    public static bool CheckPositionExists(List<PotatoToken> tokens, int targetPosition) =>
        targetPosition < tokens.Count;

    public static IEqualityExpressionAstNode CreateEqualityExpressionNode(PotatoToken leftHandSideToken,
                                                                          PotatoToken rightHandSideToken)
    {
        if (leftHandSideToken.TokenType == TokenTypes.IntegerLiteral
         && rightHandSideToken.TokenType == TokenTypes.IntegerLiteral)
        {
            return CreateIntegerEqualityExpressionNode(
                leftHandSideToken,
                rightHandSideToken,
                TokenTypes.Sign_DoubleEquality);
        }

        string msg = "";
        throw new PotatoParserException();
    }

    private static IEqualityExpressionAstNode CreateIntegerEqualityExpressionNode(
        PotatoToken leftHandSideToken,
        PotatoToken rightHandSideToken,
        string signDoubleEquality) => new IntegerTypedEqualityExpressionAstNode {
        Operation = signDoubleEquality,
        LeftSide = int.Parse(leftHandSideToken.Value),
        RightSide = int.Parse(rightHandSideToken.Value),
        Result = int.Parse(leftHandSideToken.Value) == int.Parse(rightHandSideToken.Value),
    };

    public static void ThrowParseException(string msg, int lineNumber, int caretPosition)
    {
        string message = $"Error happened while parsing! Details: \n" +
                         $"{msg}; line number: {lineNumber}; caret position: {caretPosition}";
        throw new PotatoParserException(message);
    }
}
