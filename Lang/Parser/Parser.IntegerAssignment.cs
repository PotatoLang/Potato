namespace Potato.Parser;

using AstNodes;

public partial class Parser
{
    private (PotatoAstNode Node, int ContinuationPosition) CreateIntegerAssignmentNode(
        int actualReadPosition,
        List<PotatoToken> tokens)
    {

        PotatoToken integerTypeToken = tokens[actualReadPosition];
        if (integerTypeToken.TokenType != TokenTypes.Keyword_Integer)
        {
            string msg = $"Unexpected token type: {integerTypeToken.TokenType}; " +
                         $"The token is: {integerTypeToken.Value}; " +
                         $"Expected token was: {TokenTypes.Keyword_Integer}; " +
                         $"Line: {integerTypeToken.LineNumber}, character: {actualReadPosition}";
            throw new PotatoParserException(msg);
        }

        PotatoToken identifierToken = null;
        if (actualReadPosition + 1 < tokens.Count)
        {
            identifierToken = tokens[actualReadPosition + 1];
            if (identifierToken.TokenType != TokenTypes.Identifier)
            {
                string msg = $"Unexpected token type: {identifierToken.TokenType}; " +
                             $"The token is: {identifierToken.Value}; " +
                             $"Expected token was: {TokenTypes.Identifier}; " +
                             $"Line: {identifierToken.LineNumber}, character: {actualReadPosition + 1}";
                throw new PotatoParserException(msg);
            }
            if (string.IsNullOrEmpty(identifierToken.Value) || string.IsNullOrWhiteSpace(identifierToken.Value))
            {
                string msg = $"Expected identifier name, but received: {identifierToken.TokenType}; " +
                             $"Line: {identifierToken.LineNumber}, character: {actualReadPosition + 1}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"Expected identifier for value assignment, but haven't received any. " +
                         $"line: {integerTypeToken.LineNumber}, position: {actualReadPosition + 1}";
            throw new PotatoParserException(msg);
        }

        PotatoToken assignmentSignToken = null;
        if (actualReadPosition + 2 < tokens.Count)
        {
            assignmentSignToken = tokens[actualReadPosition + 2];
            if (assignmentSignToken.TokenType != TokenTypes.Sign_Assignment)
            {
                string msg = $"Unexpected token type: {assignmentSignToken.TokenType}; " +
                             $"The token is: {assignmentSignToken.Value}; " +
                             $"Expected token was: {TokenTypes.Sign_Assignment}; " +
                             $"Line: {assignmentSignToken.LineNumber}, character: {actualReadPosition + 2}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"Expected assignment sign (=) for value assignment, but didn't received any. " +
                         $"Line: {integerTypeToken.LineNumber}, position: {actualReadPosition + 2}";
            throw new PotatoParserException(msg);
        }

        PotatoToken integerValueToken = null;
        if (actualReadPosition + 3 < tokens.Count)
        {
            integerValueToken = tokens[actualReadPosition + 3];
            if (integerValueToken.TokenType != TokenTypes.Value_Integer)
            {
                string msg = $"Unexpected token type: {integerValueToken.TokenType}; " +
                             $"The token is: {integerValueToken.Value}; " +
                             $"Expected token was: {TokenTypes.Sign_Assignment}; " +
                             $"Line: {integerValueToken.LineNumber}, character: {actualReadPosition + 3}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"Expected the integer value for integer value assignment, but didn't receive any. " +
                         $"Line: {integerTypeToken.LineNumber}, position: {actualReadPosition + 3}";
            throw new PotatoParserException(msg);
        }

        PotatoToken semicolonToken = null;
        if (actualReadPosition + 4 < tokens.Count)
        {
            semicolonToken = tokens[actualReadPosition + 4];
            if (semicolonToken.TokenType != TokenTypes.Sign_Semicolon)
            {
                string msg = $"Unexpected token type: {semicolonToken.TokenType}; " +
                             $"The token is: {semicolonToken.Value}; " +
                             $"Expected token was: {TokenTypes.Sign_Semicolon}; " +
                             $"Line: {semicolonToken.LineNumber}, character: {actualReadPosition + 4}";
                throw new PotatoParserException(msg);
            }
        }
        else
        {
            string msg = $"Expected semicolon (;) for integer value assignment, but didn't receive any. " +
                         $"Line: {integerTypeToken.LineNumber}, position: {actualReadPosition + 4}";
            throw new PotatoParserException(msg);
        }

        return (
            new PotatoAstNode {

                PotatoDatatype = PotatoDatatypes.Integer,
                VariableName = identifierToken.Value,
                IntValue = int.Parse(integerValueToken.Value),
            },
            actualReadPosition + 4);
    }
}
