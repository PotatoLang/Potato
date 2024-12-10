namespace Potato.AstNodes;

public class IntegerAssignmentStatementNode : ITypedAssignmentStatementNode<int>
{
    public string VariableLiteral { get; set; }
    public IExpressionNode? VariableExpressionNode { get; set; }
    public int Value { get; set; }
}

public interface ITypedAssignmentStatementNode<PotatoType> : IAssignmentStatementNode
{
    PotatoType Value { get; set; }
}

public interface IAssignmentStatementNode
{
    string VariableLiteral { get; set; }
    IExpressionNode? VariableExpressionNode { get; set; }
}
