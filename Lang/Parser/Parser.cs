namespace Potato.Parser;

using AstNodes;

using Lexer;

public partial class Parser
{
    private Lexer Lexer => new();

    public PotatoAstNode Parse(IEnumerable<string> sourceCode)
    {
        List<PotatoToken> tokens = Lexer.Lexing(sourceCode);
        return ParseTokens(tokens);
    }

    private PotatoAstNode ParseTokens(List<PotatoToken> tokens)
    {
        PotatoAstNode potatoAstNode = new();
        for (int actualPosition = 0; actualPosition < tokens.Count; actualPosition++)
        {
            switch (tokens[actualPosition].TokenType)
            {
                case TokenTypes.Keyword_Integer:
                    (PotatoAstNode Node, int ContinuationPosition) integerAssignmentNode = CreateIntegerAssignmentNode(
                        actualPosition,
                        tokens);
                    actualPosition = integerAssignmentNode.ContinuationPosition;
                    potatoAstNode.Nodes.Add(integerAssignmentNode.Node);
                    break;

                case TokenTypes.Keyword_Boolean:
                    (PotatoAstNode Node, int ContinuationPosition) booleanAssignmentNode = CreateBooleanAssignmentNode(
                        actualPosition,
                        tokens);
                    actualPosition = booleanAssignmentNode.ContinuationPosition;
                    potatoAstNode.Nodes.Add(booleanAssignmentNode.Node);
                    break;

                case TokenTypes.Keyword_String:
                    (PotatoAstNode Node, int ContinuationPosition) stringAssignmentNode = CreateStringAssignmentNode(
                        actualPosition,
                        tokens);
                    actualPosition = stringAssignmentNode.ContinuationPosition;
                    potatoAstNode.Nodes.Add(stringAssignmentNode.Node);
                    break;

                default:
                    string msg = $"The character is not one can start an assignment: " +
                                 $"Character: {tokens[actualPosition].Value} " +
                                 $"at {tokens[actualPosition].LineNumber}.";
                    throw new PotatoParserException(msg);
            }
        }
        return potatoAstNode;
    }
}
