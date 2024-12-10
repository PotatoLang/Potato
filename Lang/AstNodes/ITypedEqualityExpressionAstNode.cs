namespace Potato.AstNodes;

public class IntegerTypedEqualityExpressionAstNode : ITypedEqualityExpressionAstNode<int>
{
    public string Operation { get; init; }
    public bool Result { get; init; }

    public PotatoAstNodeType NodeType { get; }
    public List<IPotatoAstNode> Nodes { get; }
    public List<IAssignmentStatementNode> VariableAssignments { get; }
    public string LiteralValue { get; init; }
    public int LeftSide { get; set; }
    public int RightSide { get; set; }
}

public interface ITypedEqualityExpressionAstNode<PotatoType> : IEqualityExpressionAstNode
{
    PotatoType LeftSide { get; set; }
    PotatoType RightSide { get; set; }
}

public interface IEqualityExpressionAstNode : IPotatoAstNode
{
}
