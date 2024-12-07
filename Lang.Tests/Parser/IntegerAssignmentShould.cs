namespace Potato.Tests.Parser;

using AstNodes;

using FluentAssertions;

using Potato.Parser;

using Parser = Potato.Parser.Parser;

public class IntegerAssignmentShould : TestBase
{
    private readonly Parser _parser = new();

    public static IEnumerable<object[]> PositiveCasesTestData()
    {
        yield return new object[] {
            "Integer identifier = 1;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        PotatoDatatype = PotatoDatatypes.Integer,
                        IntValue = 1,
                        VariableName = "identifier",
                    },
                },
            },
        };
        yield return new object[] {
            "Integer identifier = 1111;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        PotatoDatatype = PotatoDatatypes.Integer,
                        IntValue = 1111,
                        VariableName = "identifier",
                    },
                },
            },
        };
        yield return new object[] {
            "Integer i = 1111;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        PotatoDatatype = PotatoDatatypes.Integer,
                        IntValue = 1111,
                        VariableName = "i",
                    },
                },
            },
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
        yield return new object[] { "i i = 2;", };
        yield return new object[] { "In i = 2;", };
        yield return new object[] { "integer i = 2;", };
        yield return new object[] { "Integer = 2;", };
        yield return new object[] { "Integer i i = 2;", };
        yield return new object[] { "Integer i 2;", };
        yield return new object[] { "Integer ii 2;", };
        yield return new object[] { "Integer i = 2", };
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
