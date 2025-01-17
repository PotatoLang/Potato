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

    private PotatoToken Tokenize(string tokenCandidate, int lineNumber)
    {

        if (IsNumber(tokenCandidate))
        {
            return new PotatoToken(TokenTypesEnum.IntegerLiteral, tokenCandidate, lineNumber, tokenCandidate);
        }

        switch (tokenCandidate)
        {
            case TokenTypes.Keyword_String:
                return new PotatoToken(TokenTypesEnum.Keyword_String, tokenCandidate, lineNumber, "");

            case TokenTypes.Keyword_Integer:
                return new PotatoToken(TokenTypesEnum.Keyword_Integer, tokenCandidate, lineNumber, "");

            case TokenTypes.Keyword_Boolean:
                return new PotatoToken(TokenTypesEnum.Keyword_Boolean, tokenCandidate, lineNumber, "");

            case TokenTypes.Keyword_Boolean_True:
                return new PotatoToken(TokenTypesEnum.Value_Boolean, tokenCandidate, lineNumber, tokenCandidate);

            case TokenTypes.Keyword_Boolean_False:
                return new PotatoToken(TokenTypesEnum.Value_Boolean, tokenCandidate, lineNumber, tokenCandidate);

            case TokenTypes.Sign_Assignment:
                return new PotatoToken(TokenTypesEnum.Sign_Assignment, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_Semicolon:
                return new PotatoToken(TokenTypesEnum.Sign_Semicolon, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_DoubleQuote:
                return new PotatoToken(TokenTypesEnum.Sign_DoubleQuote, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_BangEquality:
                return new PotatoToken(TokenTypesEnum.Sign_BangEquality, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_DoubleEquality:
                return new PotatoToken(TokenTypesEnum.Sign_DoubleEquality, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_OpenParentheses:
                return new PotatoToken(TokenTypesEnum.Sign_OpenParentheses, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_CloseParentheses:
                return CreateCloseParenthesesToken(tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_Addition:
                return new PotatoToken(TokenTypesEnum.Sign_Addition, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_Subtraction:
                return new PotatoToken(TokenTypesEnum.Sign_Subtraction, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_Multiplication:
                return new PotatoToken(TokenTypesEnum.Sign_Multiplication, tokenCandidate, lineNumber, "");

            case TokenTypes.Sign_Division:
                return new PotatoToken(TokenTypesEnum.Sign_Division, tokenCandidate, lineNumber, "");

            default:
                return new PotatoToken(TokenTypesEnum.Identifier, tokenCandidate, lineNumber, tokenCandidate);
        }
    }

    public PotatoToken CreateCloseParenthesesToken(string tokenCandidate,
                                                   int lineNumber,
                                                   string tokenValue) =>
        new(TokenTypesEnum.Sign_CloseParentheses, tokenCandidate, lineNumber, tokenValue);

    private PotatoToken CreateStringTypeValueToken(string value, int lineNumber) =>
        new(TokenTypesEnum.StringLiteral, value, lineNumber, value);

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
