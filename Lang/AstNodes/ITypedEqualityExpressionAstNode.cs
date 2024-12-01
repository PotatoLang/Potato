namespace Potato.AstNodes;

public interface ITypedEqualityExpressionAstNode<PotatoType> : IEqualityExpressionAstNode
{
    PotatoType LeftSide { get; set; }
    PotatoType RightSide { get; set; }
}

public interface IEqualityExpressionAstNode : IPotatoAstNode
{
}
