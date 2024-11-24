namespace Potato.Tests.Parser;

using AstNodes;

using FluentAssertions;

using Potato.Parser;

using Parser = Potato.Parser.Parser;

public class BooleanAssignmentShould : TestBase
{
    private readonly Parser _parser = new();

    public static IEnumerable<object[]> PositiveCasesTestData()
    {
        yield return new object[] {
            "Boolean identifier = true;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        PotatoDatatype = PotatoDatatypes.Boolean,
                        BooleanValue = true,
                        VariableName = "identifier",
                    },
                },
            },
        };
        yield return new object[] {
            "Boolean identifier = false;",
            new PotatoAstNode {
                Nodes = {
                    new PotatoAstNode {
                        PotatoDatatype = PotatoDatatypes.Boolean,
                        BooleanValue = false,
                        VariableName = "identifier",
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
        yield return new object[] { "b i = true;", };
        yield return new object[] { "bo i = false;", };
        yield return new object[] { "boolean i = true;", };
        yield return new object[] { "Boolean = 2;", };
        yield return new object[] { "Boolean i i = true;", };
        yield return new object[] { "Boolean i true;", };
        yield return new object[] { "Boolean ii false;", };
        yield return new object[] { "Boolean i = \"true\";", };
        yield return new object[] { "Boolean i = \"false\";", };
        yield return new object[] { "Boolean i = false", };
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
