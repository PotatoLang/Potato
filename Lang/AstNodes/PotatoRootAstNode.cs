namespace Potato.AstNodes;

public class PotatoRootAstNode : IPotatoAstNode
{
    public List<IPotatoAstNode> Nodes { get; init; } = new();
    public List<IAssignmentStatementNode> VariableAssignments { get; } = new();
    public string LiteralValue { get; init; }
    public PotatoAstNodeType NodeType { get; init; } = PotatoAstNodeType.Root;
}

public interface IPotatoAstNode
{
    PotatoAstNodeType NodeType { get; }
    List<IPotatoAstNode> Nodes { get; }
    List<IAssignmentStatementNode> VariableAssignments { get; }
    string LiteralValue { get; init; }
}

public enum PotatoAstNodeType
{
    Root,
    Expression,
    Statement,
    Invalid,
}
