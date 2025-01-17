namespace Potato.Tests.Parser.Variable.Expressions.Integer;

using AstNodes;

using Xunit.Abstractions;

public class SingleOperationExpression : TestBase
{
    public SingleOperationExpression(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void Assign_SingleMultiplication()
    {
        string input = "Integer integerIdentifier = 1 * 2;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Multiplication,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 1,
                            ValueLiteral = "1",
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 2,
                            ValueLiteral = "2",
                        },
                    },
                },
            },
        };
        PotatoRootAstNode result = Parser.Parse(testData);
        PrintResult(result);
        CheckResult(result, expectedResult);
    }

    [Fact]
    public void Assign_SingleDivision()
    {
        string input = "Integer integerIdentifier = 1 / 2;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Division,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 1,
                            ValueLiteral = "1",
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 2,
                            ValueLiteral = "2",
                        },
                    },
                },
            },
        };
        PotatoRootAstNode result = Parser.Parse(testData);
        PrintResult(result);
        CheckResult(result, expectedResult);
    }

    [Fact]
    public void Assign_SingleAddition()
    {
        string input = "Integer integerIdentifier = 1 + 2;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Addition,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 1,
                            ValueLiteral = "1",
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 2,
                            ValueLiteral = "2",
                        },
                    },
                },
            },
        };
        PotatoRootAstNode result = Parser.Parse(testData);
        PrintResult(result);
        CheckResult(result, expectedResult);
    }

    [Fact]
    public void Assign_SingleSubtraction()
    {
        string input = "Integer integerIdentifier = 1 - 2;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Subtraction,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 1,
                            ValueLiteral = "1",
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 2,
                            ValueLiteral = "2",
                        },
                    },
                },
            },
        };
        PotatoRootAstNode result = Parser.Parse(testData);
        PrintResult(result);
        CheckResult(result, expectedResult);
    }
}
