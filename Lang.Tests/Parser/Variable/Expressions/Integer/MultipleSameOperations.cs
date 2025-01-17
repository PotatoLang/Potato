namespace Potato.Tests.Parser.Variable.Expressions.Integer;

using AstNodes;

using Xunit.Abstractions;

public class MultipleSameOperations : TestBase
{

    public MultipleSameOperations(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }


    [Fact]
    public void Assign_ThreeAdditions()
    {
        string input = "Integer integerIdentifier = 1 + 2 + 3;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
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
    public void Assign_FourAdditions()
    {
        string input = "Integer integerIdentifier = 1 + 2 + 3 + 4;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Addition,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode =
                                new InFixExpressionNode {
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
    public void Assign_ThreeDivisions()
    {
        string input = "Integer integerIdentifier = 1 / 2 / 3;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Division,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {
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
    public void Assign_FourDivisions()
    {
        string input = "Integer integerIdentifier = 1 / 2 / 3 / 4;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Division,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {
                                TokenType = TokenTypesEnum.Sign_Division,
                                ExpressionNodeType = ExpressionNodeType.Infix,
                                LeftSideNode = new InFixExpressionNode {
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
    public void Assign_3Subtractions()
    {
        string input = "Integer integerIdentifier = 1 - 2 - 3;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
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
    public void Assign_4Subtractions()
    {
        string input = "Integer integerIdentifier = 1 - 2 - 3 - 4;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Subtraction,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode =
                                new InFixExpressionNode {
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
    public void Assign_3Multiplications()
    {
        string input = "Integer integerIdentifier = 1 * 2 * 3;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Multiplication,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {
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
    public void Assign_4Multiplications()
    {
        string input = "Integer integerIdentifier = 1 * 2 * 3 * 4;";
        IEnumerable<string> testData = ReadTestData(input);
        _testOutputHelper.WriteLine(input);
        PotatoRootAstNode expectedResult = new() {
            VariableAssignments = {
                new IntegerAssignmentStatementNode {
                    VariableLiteral = "integerIdentifier",
                    VariableExpressionNode =
                        new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Multiplication,
                            ExpressionNodeType = ExpressionNodeType.Infix,
                            LeftSideNode = new InFixExpressionNode {
                                TokenType = TokenTypesEnum.Sign_Multiplication,
                                ExpressionNodeType = ExpressionNodeType.Infix,
                                LeftSideNode = new InFixExpressionNode {
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
}
