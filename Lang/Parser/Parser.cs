namespace Potato.Parser;

using AstNodes;

using Lexer;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

public partial class Parser
{

    private readonly ILogger _logger;

    private readonly Lexer Lexer;
    private readonly int position = 0;

    public Parser(ITestOutputHelper testOutputHelper)
    {
        _logger = LoggerFactory.Create(o => { o.AddProvider(new XUnitLoggerProvider(testOutputHelper)); })
                               .CreateLogger<Parser>();

        Lexer = new Lexer(testOutputHelper);
    }

    public Parser()
    {
        _logger = LoggerFactory.Create(o => { o.AddConsole(); })
                               .CreateLogger<Parser>();
    }

    public PotatoRootAstNode Parse(IEnumerable<string> sourceCode)
    {
        List<PotatoToken> tokens = Lexer.Lexing(sourceCode);
        return ParseTokens(tokens);
    }

    private PotatoRootAstNode ParseTokens(List<PotatoToken> tokens)
    {
        PotatoRootAstNode potatoRootRootNode = new();

        (IAssignmentStatementNode VariableStatementNodes, int ContinuationPosition) variableStatements =
            ParseVariableAssignments(tokens, position);
        potatoRootRootNode.VariableAssignments.Add(variableStatements.VariableStatementNodes);

        return potatoRootRootNode;
    }
}

public interface IPotatoParser
{
    PotatoRootAstNode Parse();
}
