namespace Potato.Parser;

using AstNodes;

public partial class Parser
{

    /// <summary>
    ///     Parses single line boolean assignment and returns the corresponding <see cref="PotatoAstNode" />;
    /// </summary>
    /// <example>
    ///     A single line Boolean assignment looks like the following:
    ///     <code>
    /// Boolean booleanIdentifier = true;
    /// Boolean booleanIdentifier = false;
    /// </code>
    /// </example>
    /// <param name="actualPosition"></param>
    /// <param name="tokens"></param>
    /// <returns></returns>
    /// <exception cref="PotatoParserException"></exception>
    private (PotatoAstNode Node, int ContinuationPosition) CreateBooleanAssignmentNode(
        int actualPosition,
        List<PotatoToken> tokens)
    {
        string operationName = "Single line boolean assignment parsing";
        PotatoToken booleanToken = tokens[actualPosition];
        if (booleanToken.TokenType != TokenTypes.Keyword_Boolean)
        {
            string msg = $"During {operationName} {TokenTypes.Keyword_Boolean} was expected but received " +
                         $"{booleanToken.TokenType} instead. " +
                         $"Line: {booleanToken.LineNumber}, position: {actualPosition}";
            throw new PotatoParserException(msg);
        }

        PotatoToken booleanIdentifierToken = null;
        if (actualPosition + 1 < tokens.Count)
        {
            booleanIdentifierToken = tokens[actualPosition + 1];
            if (booleanIdentifierToken.TokenType != TokenTypes.Identifier)
            {
                string msg = $"During {operationName} {TokenTypes.Identifier} was expected but received " +
                             $"{booleanIdentifierToken.TokenType} instead. " +
                             $"Line: {booleanIdentifierToken.LineNumber}, position: {actualPosition + 1}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} expected further values, but there were no any. " +
                         $"Line: {booleanToken.LineNumber}, position: {actualPosition + 1}";
            throw new PotatoParserException(msg);
        }

        PotatoToken assignmentToken = null;
        if (actualPosition + 2 < tokens.Count)
        {
            assignmentToken = tokens[actualPosition + 2];
            if (assignmentToken.TokenType != TokenTypes.Sign_Assignment)
            {
                string msg = $"During {operationName} {TokenTypes.Sign_Assignment} was expected but received " +
                             $"{assignmentToken.TokenType} instead. " +
                             $"Line: {booleanToken.LineNumber}, position: {actualPosition + 2}.";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} expected further values, but there were no any. " +
                         $"Line: {booleanToken.LineNumber}, position: {actualPosition + 2}";
            throw new PotatoParserException(msg);
        }

        PotatoToken booleanValueToken = null;
        if (actualPosition + 3 < tokens.Count)
        {
            booleanValueToken = tokens[actualPosition + 3];
            if (booleanValueToken.TokenType != TokenTypes.Value_Boolean)
            {
                string msg = $"During {operationName} {TokenTypes.Value_Boolean} was expected but received " +
                             $"{booleanValueToken.TokenType} instead. " +
                             $"Line: {booleanToken.LineNumber}, position: {actualPosition + 3}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} expected further values, but there were no any. " +
                         $"Line: {booleanToken.LineNumber}, position: {actualPosition + 3}";
            throw new PotatoParserException(msg);
        }

        PotatoToken semicolonToken = null;
        if (actualPosition + 4 < tokens.Count)
        {
            semicolonToken = tokens[actualPosition + 4];
            if (semicolonToken.TokenType != TokenTypes.Sign_Semicolon)
            {
                string msg = $"During {operationName} {TokenTypes.Sign_Semicolon} was expected but received " +
                             $"{semicolonToken.TokenType} instead. " +
                             $"Line: {booleanToken.LineNumber}, position: {actualPosition + 4}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} expected further values, but there were no any. " +
                         $"Line: {booleanToken.LineNumber}, position: {actualPosition + 4}";
            throw new PotatoParserException(msg);
        }

        return (
            new PotatoAstNode {
                PotatoDatatype = PotatoDatatypes.Boolean,
                VariableName = booleanIdentifierToken.Value,
                BooleanValue = bool.Parse(booleanValueToken.Value),
            },
            actualPosition + 4
        );
    }
}
