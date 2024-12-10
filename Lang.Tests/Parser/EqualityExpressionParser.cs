namespace Potato.Tests.Parser;

using AstNodes;

using Xunit.Abstractions;

public class EqualityExpressionParser : TestBase
{
    public EqualityExpressionParser(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public static IEnumerable<object[]> CorrectCasesData11()
    {
        yield return new object[] {
            "111 == 111;",
            new PotatoRootAstNode {
                Nodes = new List<IPotatoAstNode> {
                    new IntegerTypedEqualityExpressionAstNode {
                        RightSide = 111,
                        LeftSide = 111,
                        Operation = TokenTypes.Sign_DoubleEquality,
                        Result = true,
                    },
                },
            },
        };
    }

    public void CorrectCases(string input, PotatoRootAstNode expectedResult)
    {
        IEnumerable<string> sourceCode = ReadTestData(input);
        PotatoRootAstNode result = Parser.Parse(sourceCode);
        CheckResult(result, expectedResult);
    }
}
