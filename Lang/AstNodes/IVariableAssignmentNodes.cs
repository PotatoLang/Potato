namespace Potato.AstNodes;

public class IntegerVariableAssignmentNode : ITypedVariableAssignmentNode<int>
{
    public PotatoAstNodeType NodeType { get; }
    public List<IPotatoAstNode> Nodes { get; }
    public string LiteralValue { get; init; }
    public string LeftSide { get; set; }
    public ITypedExpressionNode<int> RightSide { get; set; }
}

public interface ITypedVariableAssignmentNode<PotatoType> : IVariableAssignmentNode
{

    /// <summary>
    ///     The left side of the expression.
    ///     In variable assignment it means whatever (usually the variable name) is
    ///     at left side of the assignment (=) sign.
    /// </summary>
    string LeftSide { get; set; }

    ITypedExpressionNode<PotatoType> RightSide { get; set; }
}

public interface IVariableAssignmentNode : IPotatoAstNode
{
}
