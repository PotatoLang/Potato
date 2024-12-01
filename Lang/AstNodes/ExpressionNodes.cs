namespace Potato.AstNodes;

public class IntegerVariableAssignmentExpressionNode : ITypedExpressionNode<int>
{

    public IExpressionNode LeftSideNode { get; set; }
    public IExpressionNode RightSideNode { get; set; }
    public int LeftLiteralValue { get; set; }
    public int RightLiteralValue { get; set; }
}

public interface ITypedExpressionNode<PotatoType> : IExpressionNode
{
    PotatoType LeftLiteralValue { get; set; }
    PotatoType RightLiteralValue { get; set; }
}

public interface IExpressionNode
{
    IExpressionNode LeftSideNode { get; set; }
    IExpressionNode RightSideNode { get; set; }
}
