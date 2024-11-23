namespace Potato.Parser;

using AstNodes;

public partial class Parser
{
    /// <summary>
    ///     Creates a <see cref="PotatoAstNode" /> for a string assignment.
    /// </summary>
    /// <example>
    ///     <code>
    /// String stringIdentifier = "something string";
    /// </code>
    /// </example>
    /// <param name="actualPosition"></param>
    /// <param name="tokens"></param>
    /// <returns></returns>
    /// <exception cref="PotatoParserException"></exception>
    private (PotatoAstNode Node, int ContinuationPosition) CreateStringAssignmentNode(
        int actualPosition,
        List<PotatoToken> tokens)
    {
        string operationName = "Single line string assignment";

        PotatoToken stringToken = tokens[actualPosition];
        if (stringToken.TokenType != TokenTypes.Keyword_String)
        {
            string msg = $"During {operationName} {TokenTypes.Keyword_String} was expected but received " +
                         $"{stringToken.TokenType} instead. " +
                         $"Line: {stringToken.LineNumber}, position: {actualPosition}";
            throw new PotatoParserException(msg);
        }

        PotatoToken stringIdentifierToken = null;
        if (actualPosition + 1 < tokens.Count)
        {
            stringIdentifierToken = tokens[actualPosition + 1];
            if (stringIdentifierToken.TokenType != TokenTypes.Identifier)
            {
                string msg = $"During {operationName} {TokenTypes.Identifier} was expected but received " +
                             $"{stringIdentifierToken.TokenType} instead. " +
                             $"Line: {stringToken.LineNumber}, position: {actualPosition + 1}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} further characters were expected but there were no any. " +
                         $"Line: {stringToken.LineNumber}, position: {actualPosition + 1}";
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
                             $"Line: {stringToken.LineNumber}, position: {actualPosition + 2}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} further characters were expected but there were no any. " +
                         $"Line: {stringToken.LineNumber}, position: {actualPosition + 2}";
            throw new PotatoParserException(msg);
        }

        PotatoToken openingDoubleQuoteToken = null;
        if (actualPosition + 3 < tokens.Count)
        {
            openingDoubleQuoteToken = tokens[actualPosition + 3];
            if (openingDoubleQuoteToken.TokenType != TokenTypes.Value_String)
            {
                string msg = $"During {operationName} {TokenTypes.Sign_DoubleQuote} was expected but received " +
                             $"{openingDoubleQuoteToken.TokenType} instead. " +
                             $"Line: {stringToken.LineNumber}, position: {actualPosition + 3}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} further characters were expected but there were no any. " +
                         $"Line: {stringToken.LineNumber}, position: {actualPosition + 3}";
            throw new PotatoParserException(msg);
        }

        PotatoToken stringValueToken = null;
        if (actualPosition + 4 < tokens.Count)
        {
            stringValueToken = tokens[actualPosition + 4];
            if (stringValueToken.TokenType != TokenTypes.Value_String)
            {
                string msg = $"During {operationName} {TokenTypes.Value_String} was expected but received " +
                             $"{stringValueToken.TokenType} instead. " +
                             $"Line: {stringToken.LineNumber}, position: {actualPosition + 4}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} further characters were expected but there were no any. " +
                         $"Line: {stringToken.LineNumber}, position: {actualPosition + 4}";
            throw new PotatoParserException(msg);
        }

        PotatoToken closingDoubleQuoteToken = null;
        if (actualPosition + 5 < tokens.Count)
        {
            closingDoubleQuoteToken = tokens[actualPosition + 5];
            if (closingDoubleQuoteToken.TokenType != TokenTypes.Value_String)
            {
                string msg = $"During {operationName} {TokenTypes.Sign_DoubleQuote} was expected but received " +
                             $"{closingDoubleQuoteToken.TokenType} instead. " +
                             $"Line: {stringToken.LineNumber}, position: {actualPosition + 5}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} further characters were expected but there were no any. " +
                         $"Line: {stringToken.LineNumber}, position: {actualPosition + 5}";
            throw new PotatoParserException(msg);
        }

        PotatoToken semicolonSignToken = null;
        if (actualPosition + 6 < tokens.Count)
        {
            semicolonSignToken = tokens[actualPosition + 6];
            if (semicolonSignToken.TokenType != TokenTypes.Value_String)
            {
                string msg = $"During {operationName} {TokenTypes.Sign_DoubleQuote} was expected but received " +
                             $"{semicolonSignToken.TokenType} instead. " +
                             $"Line: {stringToken.LineNumber}, position: {actualPosition + 6}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"During {operationName} further characters were expected but there were no any. " +
                         $"Line: {stringToken.LineNumber}, position: {actualPosition + 6}";
            throw new PotatoParserException(msg);
        }
        return (
            new PotatoAstNode(),
            actualPosition + 6
        );
    }
}
