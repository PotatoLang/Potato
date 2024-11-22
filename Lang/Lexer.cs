namespace Potato;

using System.Text;

public class Lexer
{
    public List<PotatoToken> Lexing(IEnumerable<string> sourceCode)
    {
        List<PotatoToken> tokens = [];
        StringBuilder actualToken = new();
        int lineNumber = 0;

        foreach (string oneLineSourceCode in sourceCode)
        {
            lineNumber++;
            for (int i = 0; i < oneLineSourceCode.ToCharArray().Length; i++)
            {
                int actualChar = i;
                int nextChar = i + 1;
                // if the actual character is not a delimiter (space and ;) then
                // it will be added to the actual token, and we will build further the token
                if (oneLineSourceCode[actualChar].ToString() != TokenTypes.Space
                 && oneLineSourceCode[actualChar].ToString() != TokenTypes.Sign_Semicolon)
                {
                    actualToken.Append(oneLineSourceCode[actualChar]);
                }

                // if the actual token is ";" it will be tokenized
                // in the previous loop the actualToken was already tokenized
                if (oneLineSourceCode[actualChar].ToString() == TokenTypes.Sign_Semicolon)
                {
                    tokens.Add(
                        Tokenize(oneLineSourceCode[actualChar].ToString(), lineNumber)
                    );
                    actualToken.Clear();
                    continue;
                }

                // when the actual char is the last one we tokenize the actualToken
                if (actualChar == oneLineSourceCode.ToCharArray().Length - 1)
                {
                    tokens.Add(Tokenize(actualToken.ToString(), lineNumber));
                    actualToken.Clear();
                    continue;
                }

                // look ahead
                if (nextChar <= oneLineSourceCode.ToCharArray().Length - 1)
                {
                    // if the next char is a delimiter char,
                    // and there is already chars in the actualToken
                    // we send the actualToken to be tokenized
                    if ((oneLineSourceCode[nextChar].ToString() == TokenTypes.Space
                      || oneLineSourceCode[nextChar].ToString() == TokenTypes.Sign_Semicolon)
                     && actualToken.Length != 0)
                    {
                        tokens.Add(
                            Tokenize(actualToken.ToString(), lineNumber)
                        );
                        actualToken.Clear();
                    }
                }
            }
        }
        return tokens;
    }

    private static PotatoToken Tokenize(string tokenCandidate, int lineNumber)
    {

        if (IsNumber(tokenCandidate))
        {
            return new PotatoToken(TokenTypes.Value_Integer, lineNumber, tokenCandidate);
        }

        switch (tokenCandidate)
        {
            case TokenTypes.Keyword_Var:
                return new PotatoToken(TokenTypes.Keyword_Var, lineNumber, "");

            case TokenTypes.Sign_Assignment:
                return new PotatoToken(TokenTypes.Sign_Assignment, lineNumber, "");

            case TokenTypes.Sign_Semicolon:
                return new PotatoToken(TokenTypes.Sign_Semicolon, lineNumber, "");

            default:
                return new PotatoToken(TokenTypes.Identifier, lineNumber, tokenCandidate);
        }
    }

    private static bool IsNumber(string tokenCandidate)
    {
        try
        {
            long.Parse(tokenCandidate);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
