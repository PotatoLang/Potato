namespace Potato.Tests.Parser;

using AstNodes;

using FluentAssertions;

using Lexer;

using Parser = Potato.Parser;

public class IntegerAssignmentShould : LexerTestBase
{
    private readonly Parser _parser = new();

    public static IEnumerable<object[]> PositiveCasesTestData()
    {
        yield return new object[] {
            "var identifier = 1;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        Datatype = Datatypes.Int,
                        IntValue = 1,
                        VariableName = "identifier"
                    }
                }
            }
        };
        yield return new object[] {
            "var identifier = 1111;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        Datatype = Datatypes.Int,
                        IntValue = 1111,
                        VariableName = "identifier"
                    }
                }
            }
        };
        yield return new object[] {
            "var i = 1111;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        Datatype = Datatypes.Int,
                        IntValue = 1111,
                        VariableName = "i"
                    }
                }
            }
        };
    }

    [Theory]
    [MemberData(nameof(PositiveCasesTestData))]
    public void ValidCases(string input, PotatoAstNode expectedResult)
    {
        IEnumerable<string> testData = ReadTestData(input);
        PotatoAstNode result = _parser.Parse(testData);
        result.Nodes.Count.Should().Be(expectedResult.Nodes.Count);
    }

    public static IEnumerable<object[]> NegativeCasesTestData()
    {
        yield return new object[] { "v i = 2;" };
        yield return new object[] { "va i = 2;" };
        yield return new object[] { "var = 2;" };
        yield return new object[] { "var i i = 2;" };
        yield return new object[] { "var i 2;" };
        yield return new object[] { "var ii 2;" };
        yield return new object[] { "var i = 2" };
    }

    [Theory]
    [MemberData(nameof(NegativeCasesTestData))]
    public void InvalidCases(string input)
    {
        IEnumerable<string> testData = ReadTestData(input);
        Action action = () => { _parser.Parse(testData); };
        action.Should().ThrowExactly<PotatoParserException>();
    }
}
