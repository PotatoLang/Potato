namespace Potato.AstNodes;

public class InFixExpressionNode : IFixExpressionNode
{
    public int Group { get; set; }
    public TokenTypesEnum TokenType { get; set; }
    public bool IsContinuationPosition { get; set; }
    public IExpressionNode ParentExpressionNode { get; set; }
    public IExpressionNode? LeftSideNode { get; set; }
    public IExpressionNode? RightSideNode { get; set; }
    public ExpressionNodeType ExpressionNodeType { get; set; } = ExpressionNodeType.Infix;
}

public interface IFixExpressionNode : IExpressionNode
{
    IExpressionNode? LeftSideNode { get; set; }
    IExpressionNode? RightSideNode { get; set; }
}

public interface ITypedLiteralValueNode<PotatoType> : ILiteralValueNode
{
    /// <summary>
    ///     The typed value and its representation.
    /// </summary>
    PotatoType Value { get; set; }
}

public interface ILiteralValueNode : IExpressionNode
{
    /// <summary>
    ///     The string representation of the value.
    /// </summary>
    string ValueLiteral { get; set; }
}

public class IntegerLiteralExpressionNode : ITypedLiteralValueNode<int>
{

    public IExpressionNode ParentExpressionNode { get; set; }
    public TokenTypesEnum TokenType { get; set; }
    public int Group { get; set; }
    public bool IsContinuationPosition { get; set; }

    /// <inheritdoc />
    public string ValueLiteral { get; set; }

    /// <inheritdoc />
    public int Value { get; set; }

    public ExpressionNodeType ExpressionNodeType { get; set; } = ExpressionNodeType.LiteralValue;
}

public class StringLiteralExpressionNode : ITypedLiteralValueNode<string>
{
    public TokenTypesEnum TokenType { get; set; }
    public int Group { get; set; }
    public bool IsContinuationPosition { get; set; }
    public ExpressionNodeType ExpressionNodeType { get; set; }
    public string ValueLiteral { get; set; }
    public IExpressionNode ParentExpressionNode { get; set; }
    public string Value { get; set; }
}

public interface IExpressionNode
{
    ExpressionNodeType ExpressionNodeType { get; set; }
    TokenTypesEnum TokenType { get; set; }
    int Group { get; set; }
    bool IsContinuationPosition { get; set; }
    IExpressionNode? ParentExpressionNode { get; set; }
}

public enum ExpressionNodeType
{
    Infix,
    Prefix,
    Postfix,
    LiteralValue,
}
