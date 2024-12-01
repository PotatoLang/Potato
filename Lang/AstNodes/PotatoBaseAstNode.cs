namespace Potato.AstNodes;

public class PotatoBaseAstNode : IPotatoAstNode
{
    public List<IPotatoAstNode> Nodes { get; init; } = new();
    public string LiteralValue { get; init; }
    public PotatoAstNodeType NodeType { get; init; }
}

public interface IPotatoAstNode
{
    PotatoAstNodeType NodeType { get; }
    List<IPotatoAstNode> Nodes { get; }
    string LiteralValue { get; init; }
}

public enum PotatoAstNodeType
{
    Root,
    Expression,
    Statement,
    Invalid,
}
