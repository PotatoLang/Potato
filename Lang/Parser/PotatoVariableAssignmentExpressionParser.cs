namespace Potato.Parser;

using AstNodes;

/// <summary>
///     Parses variable assignment expressions and return the corresponding AST nodes.
///     <example>
///         <code>
///             // assignment(type identifier assignment_sign) variableAssignmentExpression
///             Integer identifier = 4;
///             Integer identifier = 5 + 1;
///             Integer identifier = 5 + 1 + 4;
///             Integer identifier = (5 * 1) + 4;
///         </code>
///     </example>
/// </summary>
public class PotatoVariableAssignmentExpressionParser
{
    public (ITypedExpressionNode<int> AssignmentExpressionNode, int ContinuationPosition)
        ParseIntegerVariableAssignmentExpression(
            List<PotatoToken> tokens,
            int position)
    {
        IntegerVariableAssignmentNode node = new();
        for (int i = position; i < tokens.Count; i++)
        {

        }
    }
