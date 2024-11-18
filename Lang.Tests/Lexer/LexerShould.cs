namespace Potato.Tests.Lexer;

using System.Collections;
using FluentAssertions;
using Potato.Lexer;

public class LexerShould : LexerTestBase
{
  private Lexer lexer;

  public LexerShould()
  {
    lexer = new Lexer();
  }

  [Fact]
  public void Tokenize_Var()
  {
    string input = @"var;";
    IEnumerable<string> testData = ReadTestData(input);
    List<PotatoToken> result = lexer.Lexing(testData);

    List<PotatoToken> expectedResult = [new PotatoToken("var", 1, "")];
    result.Should().Equal(expectedResult);
  }

  [Fact]
  public void Tokenize_Identifier()
  {
    string input = @"var anIdentifier;";
    IEnumerable<string> testData = ReadTestData(input);
    List<PotatoToken> result = lexer.Lexing(testData);

    List<PotatoToken> expectedResult = [
      new PotatoToken("var", 1, ""),
      new PotatoToken("identifier", 1, "anIdentifier"),
    ];
    result.Should().Equal(expectedResult);
  }

}
