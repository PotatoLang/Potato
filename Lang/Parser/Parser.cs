namespace Potato.Parser;

using AstNodes;

using Lexer;

public class Parser
{
    private readonly PotatoExpressionParser _potatoExpressionParser = new();
    private readonly PotatoVariableAssignmentParser _potatoVariableAssignmentParser = new();
    private readonly int position = 0;
    private Lexer Lexer => new();

    public PotatoBaseAstNode Parse(IEnumerable<string> sourceCode)
    {
        List<PotatoToken> tokens = Lexer.Lexing(sourceCode);
        return ParseTokens(tokens);
    }

    private PotatoBaseAstNode ParseTokens(List<PotatoToken> tokens)
    {
        PotatoBaseAstNode potatoBaseRootNode = new();

        (IPotatoAstNode VariableStatementNodes, int ContinuationPosition) variableStatements =
            _potatoVariableAssignmentParser.ParseVariableAssignments(tokens, position);

        (IPotatoAstNode ExpressionNodes, int ContinuationPosition) expressions =
            _potatoExpressionParser.ParseExpressions(tokens, position);
        potatoBaseRootNode.Nodes.Add(expressions.ExpressionNodes);

        return potatoBaseRootNode;
    }
}

public interface IPotatoParser
{
    PotatoBaseAstNode Parse();
}
