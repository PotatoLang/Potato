namespace Potato.Tests.Parser;

using AstNodes;

using Xunit.Abstractions;

public class StringVariableAssignmentCases : TestBase
{

    public StringVariableAssignmentCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public void StringAssignment(string input, PotatoRootAstNode expectedResult)
    {
        IEnumerable<string> testData = ReadTestData(input);
        PotatoRootAstNode result = Parser.Parse(testData);
        PrintResult(result);
        CheckResult(result, expectedResult);
    }
}
