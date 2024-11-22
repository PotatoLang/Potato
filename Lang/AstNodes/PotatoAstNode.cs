namespace Potato.AstNodes;

public class PotatoAstNode
{
    public string VariableName { get; init; }
    public string Datatype { get; init; }
    public int IntValue { get; init; }
    public List<PotatoAstNode> Nodes { get; } = new();
}
