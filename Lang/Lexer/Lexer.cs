namespace Potato.Lexer;

using System.Text;

public class Lexer
{
  public List<PotatoToken> Lexing(IEnumerable<string> sourceCode)
  {
    List<PotatoToken> tokens = [];
    StringBuilder builder = new();
    int lineNumber = 0;

    foreach (string oneLineSourceCode in sourceCode)
    {
      lineNumber++;
      for (int i = 0; i < oneLineSourceCode.ToCharArray().Length; i++)
      {
        // when we hit space we determine the token
        if (oneLineSourceCode[i].ToString() == Tokens.Space || i == oneLineSourceCode.Length - 1)
        {
          tokens.Add(Tokenize(builder.ToString(), lineNumber));
          builder.Clear();
          continue;
        }

        builder.Append(oneLineSourceCode[i]);
      }
    }
    return tokens;
  }

  private static PotatoToken Tokenize(string tokenCandidate, int lineNumber)
  {
    switch (tokenCandidate)
    {
      case Tokens.Var:
        return new PotatoToken(Tokens.Var, lineNumber, "");

      default:
        return new PotatoToken(Tokens.Identifier, lineNumber, tokenCandidate);
    }

  }

}
