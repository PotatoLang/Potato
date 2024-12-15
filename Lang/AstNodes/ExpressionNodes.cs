namespace Potato.AstNodes;

public class InFixExpressionNode : IFixExpressionNode
{
    public IExpressionNode? LeftSideNode { get; set; }
    public IExpressionNode? RightSideNode { get; set; }
    public TokenTypesEnum TokenType { get; set; }
    public ExpressionNodeType ExpressionNodeType { get; set; } = ExpressionNodeType.Infix;
}

public interface IFixExpressionNode : IExpressionNode
{
    IExpressionNode? LeftSideNode { get; set; }
    IExpressionNode? RightSideNode { get; set; }
    TokenTypesEnum TokenType { get; set; }
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
    string StringValueLiteral { get; set; }

    /// <summary>
    ///     The token type representation.
    /// </summary>
    string StringTokenLiteral { get; set; }
}

public class IntegerLiteralExpressionNode : ITypedLiteralValueNode<int>
{
    /// <inheritdoc />
    public string StringValueLiteral { get; set; }

    /// <inheritdoc />
    public string StringTokenLiteral { get; set; }

    /// <inheritdoc />
    public int Value { get; set; }

    public ExpressionNodeType ExpressionNodeType { get; set; } = ExpressionNodeType.LiteralValue;
}

public class StringLiteralExpressionNode : ITypedLiteralValueNode<string>
{
    public ExpressionNodeType ExpressionNodeType { get; set; }
    public string StringValueLiteral { get; set; }
    public string StringTokenLiteral { get; set; }
    public string Value { get; set; }
}

public interface IExpressionNode
{
    ExpressionNodeType ExpressionNodeType { get; set; }
}

public enum ExpressionNodeType
{
    Infix,
    Prefix,
    Postfix,
    LiteralValue,
}
