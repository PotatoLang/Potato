namespace Potato.AstNodes;

using Parser;

public class PotatoAstNode
{
    public string VariableName { get; init; }
    public PotatoDatatypes PotatoDatatype { get; init; }
    public int IntValue { get; init; }
    public List<PotatoAstNode> Nodes { get; } = new();
    public bool BooleanValue { get; set; }
}
