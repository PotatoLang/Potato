namespace Potato.Lexer;

using System.Text;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

public class Lexer
{

    private readonly ILogger _logger;

    public Lexer(ITestOutputHelper testOutputHelper)
    {
        _logger = LoggerFactory.Create(o => { o.AddDebug(); }).CreateLogger(nameof(Lexer));
    }

    public Lexer()
    {
        _logger = LoggerFactory.Create(o => { o.AddConsole(); }).CreateLogger(nameof(Lexer));
    }

    public List<PotatoToken> Lexing(IEnumerable<string> sourceCode)
    {
        List<PotatoToken> tokens = [];
        StringBuilder actualToken = new();
        int lineNumber = 0;

        foreach (string oneLineSourceCode in sourceCode)
        {
            lineNumber++;
            LexerMode lexerMode = LexerMode.Default;
            for (int i = 0; i < oneLineSourceCode.ToCharArray().Length; i++)
            {
                int actualPosition = i;
                int nextPosition = i + 1;

                // the assumption is that when a string value is created there are at lest 2 " characters in line
                // the first one switches the lexerMode and the lexer puts everything in a single value
                // the second one switches the lexerMode back to Default and sends the string to tokenize
                // in this case the tokenizer has a shortcut
                // TODO: escaped " character management
                if (oneLineSourceCode[actualPosition].ToString() == "\"" && lexerMode == LexerMode.Default)
                {
                    lexerMode = LexerMode.StringVariableValue;
                    if (actualToken.Length > 0)
                    {
                        tokens.Add(Tokenize(actualToken.ToString(), lineNumber));
                        actualToken.Clear();
                    }
                    tokens.Add(Tokenize(oneLineSourceCode[actualPosition].ToString(), lineNumber));
                    continue;
                }

                // when the lexer is already string type value processing mode
                // and the actual character is a ", meaning we reached the end of the string contect
                // the lexer will be switched back to default mode and the string type value will be tokenized
                // and the actual character also will be tokenized
                if (oneLineSourceCode[actualPosition].ToString() == "\"" && lexerMode == LexerMode.StringVariableValue)
                {
                    if (actualToken.Length > 0)
                    {
                        tokens.Add(CreateStringTypeValueToken(actualToken.ToString(), lineNumber));
                        actualToken.Clear();
                    }
                    tokens.Add(Tokenize(oneLineSourceCode[actualPosition].ToString(), lineNumber));
                    lexerMode = LexerMode.Default;
                    continue;
                }

                if (lexerMode == LexerMode.StringVariableValue)
                {
                    actualToken.Append(oneLineSourceCode[i]);
                    continue;
                }

                // when the actual char is "!" and the following is "=" ==> tokenize
                // when the actual char is "=" and the following is "=" ==> tokenize
                if (actualToken.Length == 0 && nextPosition <= oneLineSourceCode.Length - 1)
                {
                    if (oneLineSourceCode[actualPosition].ToString() == TokenTypes.Sign_Bang
                     && oneLineSourceCode[nextPosition].ToString() == TokenTypes.Sign_Assignment
                     || oneLineSourceCode[actualPosition].ToString() == TokenTypes.Sign_Assignment
                     && oneLineSourceCode[nextPosition].ToString() == TokenTypes.Sign_Assignment)
                    {
                        actualToken.Append(oneLineSourceCode[actualPosition])
                                   .Append(oneLineSourceCode[nextPosition]);
                        tokens.Add(Tokenize(actualToken.ToString(), lineNumber));
                        // we move the pointer ahead after the second char.
                        i = nextPosition;
                        actualToken.Clear();
                        continue;
                    }
                }

                // if the actual token is ";" it will cause the following:
                // the already populated actualToken will be tokenized
                // the ";" character also will be tokenized
                if (oneLineSourceCode[actualPosition].ToString() == TokenTypes.Sign_Semicolon
                 || oneLineSourceCode[actualPosition].ToString() == TokenTypes.Sign_Assignment
                 || oneLineSourceCode[actualPosition].ToString() == TokenTypes.Sign_OpenParentheses
                 || oneLineSourceCode[actualPosition].ToString() == TokenTypes.Sign_CloseParentheses)
                {
                    if (actualToken.Length > 0)
                    {
                        tokens.Add(Tokenize(actualToken.ToString(), lineNumber));
                        actualToken.Clear();
                    }
                    tokens.Add(Tokenize(oneLineSourceCode[actualPosition].ToString(), lineNumber));
                    continue;
                }

                // if we reached a space (" ") character then
                // if actualToken has something in it, it will be tokenized
                // if not we skip doing anything to space (" "), we swallow it
                if (oneLineSourceCode[actualPosition].ToString() == TokenTypes.Space)
                {
                    if (actualToken.Length > 0)
                    {
                        tokens.Add(Tokenize(actualToken.ToString(), lineNumber));
                        actualToken.Clear();
                    }
                    continue;
                }

                // when the actual char is the last one we tokenize the actualToken
                if (actualPosition == oneLineSourceCode.ToCharArray().Length - 1)
                {
                    actualToken.Append(oneLineSourceCode[actualPosition]);
                    tokens.Add(Tokenize(actualToken.ToString(), lineNumber));
                    actualToken.Clear();
                    continue;
                }

                // swallowing space (" ") happens earlier
                actualToken.Append(oneLineSourceCode[actualPosition]);
            }
        }
        return tokens;
    }

    private PotatoToken CreateStringTypeValueToken(string value, int lineNumber) =>
        new(TokenTypes.Value_String, lineNumber, value);

    private static PotatoToken Tokenize(string tokenCandidate, int lineNumber)
    {

        if (IsNumber(tokenCandidate))
        {
            return new PotatoToken(TokenTypes.IntegerLiteral, lineNumber, tokenCandidate);
        }

        switch (tokenCandidate)
        {
            case TokenTypes.Keyword_String:
                return new PotatoToken(TokenTypes.Keyword_String, lineNumber, "");

            case TokenTypes.Keyword_Integer:
                return new PotatoToken(TokenTypes.Keyword_Integer, lineNumber, "");

            case TokenTypes.Keyword_Boolean:
                return new PotatoToken(TokenTypes.Keyword_Boolean, lineNumber, "");

            case TokenTypes.Keyword_Boolean_True:
                return new PotatoToken(TokenTypes.Value_Boolean, lineNumber, tokenCandidate);

            case TokenTypes.Keyword_Boolean_False:
                return new PotatoToken(TokenTypes.Value_Boolean, lineNumber, tokenCandidate);

            case TokenTypes.Sign_Assignment:
                return new PotatoToken(TokenTypes.Sign_Assignment, lineNumber, "");

            case TokenTypes.Sign_Semicolon:
                return new PotatoToken(TokenTypes.Sign_Semicolon, lineNumber, "");

            case TokenTypes.Sign_DoubleQuote:
                return new PotatoToken(TokenTypes.Sign_DoubleQuote, lineNumber, "");

            case TokenTypes.Sign_BangEquality:
                return new PotatoToken(TokenTypes.Sign_BangEquality, lineNumber, "");

            case TokenTypes.Sign_DoubleEquality:
                return new PotatoToken(TokenTypes.Sign_DoubleEquality, lineNumber, "");

            case TokenTypes.Sign_OpenParentheses:
                return new PotatoToken(TokenTypes.Sign_OpenParentheses, lineNumber, "");

            case TokenTypes.Sign_CloseParentheses:
                return new PotatoToken(TokenTypes.Sign_CloseParentheses, lineNumber, "");

            case TokenTypes.Sign_Addition:
                return new PotatoToken(TokenTypes.Sign_Addition, lineNumber, "");

            case TokenTypes.Sign_Subtraction:
                return new PotatoToken(TokenTypes.Sign_Subtraction, lineNumber, "");

            case TokenTypes.Sign_Multiplication:
                return new PotatoToken(TokenTypes.Sign_Multiplication, lineNumber, "");

            case TokenTypes.Sign_Division:
                return new PotatoToken(TokenTypes.Sign_Division, lineNumber, "");

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

public enum LexerMode
{
    Default,
    StringVariableValue,
}
