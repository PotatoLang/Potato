namespace Potato.Tests.Parser.Variable.Expressions.Integer;

using System.Diagnostics.CodeAnalysis;

using AstNodes;

using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
public class GroupedToTheExtreme : TestBase
{

    public GroupedToTheExtreme(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void Two_Subtractions()
    {
        string input = "Integer integerIdentifier = ((1 - 2) - 3);";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Subtraction,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new InFixExpressionNode {
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
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 3,
                            ValueLiteral = "3",
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
    public void Three_Subtractions()
    {
        string input = "Integer integerIdentifier = (((1 - 2) - 3) - 4);";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Subtraction,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new InFixExpressionNode {

                            TokenType = TokenTypesEnum.Sign_Subtraction,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {
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
                            RightSideNode = new IntegerLiteralExpressionNode {
                                TokenType = TokenTypesEnum.IntegerLiteral,
                                Value = 3,
                                ValueLiteral = "3",
                            },
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 4,
                            ValueLiteral = "4",
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
    public void Four_Subtractions()
    {
        string input = "Integer integerIdentifier = ((((1 - 2) - 3) - 4) - 5);";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Subtraction,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Subtraction,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {

                                TokenType = TokenTypesEnum.Sign_Subtraction,
                                ExpressionNodeType = ExpressionNodeType.Infix,
                                LeftSideNode = new InFixExpressionNode {
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
                                RightSideNode = new IntegerLiteralExpressionNode {
                                    TokenType = TokenTypesEnum.IntegerLiteral,
                                    Value = 3,
                                    ValueLiteral = "3",
                                },
                            },
                            RightSideNode = new IntegerLiteralExpressionNode {
                                TokenType = TokenTypesEnum.IntegerLiteral,
                                Value = 4,
                                ValueLiteral = "4",
                            },
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 5,
                            ValueLiteral = "5",
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
    public void Two_Additions()
    {
        string input = "Integer integerIdentifier = ((1 + 2) + 3);";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Addition,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new InFixExpressionNode {
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
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 3,
                            ValueLiteral = "3",
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
    public void Three_Additions()
    {
        string input = "Integer integerIdentifier = (((1 + 2) + 3) + 4);";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Addition,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new InFixExpressionNode {

                            TokenType = TokenTypesEnum.Sign_Addition,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {
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
                            RightSideNode = new IntegerLiteralExpressionNode {
                                TokenType = TokenTypesEnum.IntegerLiteral,
                                Value = 3,
                                ValueLiteral = "3",
                            },
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 4,
                            ValueLiteral = "4",
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
    public void Four_Additions()
    {
        string input = "Integer integerIdentifier = ((((1 + 2) + 3) + 4) + 5);";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode = new InFixExpressionNode {
                        TokenType = TokenTypesEnum.Sign_Addition,
                        ExpressionNodeType = ExpressionNodeType.Infix,
                        LeftSideNode = new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Addition,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {

                                TokenType = TokenTypesEnum.Sign_Addition,
                                ExpressionNodeType = ExpressionNodeType.Infix,
                                LeftSideNode = new InFixExpressionNode {
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
                                RightSideNode = new IntegerLiteralExpressionNode {
                                    TokenType = TokenTypesEnum.IntegerLiteral,
                                    Value = 3,
                                    ValueLiteral = "3",
                                },
                            },
                            RightSideNode = new IntegerLiteralExpressionNode {
                                TokenType = TokenTypesEnum.IntegerLiteral,
                                Value = 4,
                                ValueLiteral = "4",
                            },
                        },
                        RightSideNode = new IntegerLiteralExpressionNode {
                            TokenType = TokenTypesEnum.IntegerLiteral,
                            Value = 5,
                            ValueLiteral = "5",
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
