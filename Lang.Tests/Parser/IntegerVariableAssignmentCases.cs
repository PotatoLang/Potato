namespace Potato.Tests.Parser;

using AstNodes;

using Xunit.Abstractions;

public class IntegerVariableAssignmentCases : TestBase
{
    public IntegerVariableAssignmentCases(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    public static IEnumerable<object[]> IntegerAssignmentCasesTestData()
    {
        // yield return [
        //     "Integer integerIdentifier = 1;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new IntegerLiteralExpressionNode {
        //                     Value = 1,
        //                     StringTokenLiteral = TokenTypes.IntegerLiteral,
        //                     StringValueLiteral = "1",
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 * 2;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 1,
        //                         StringTokenLiteral = "1",
        //                         StringValueLiteral = "1",
        //                     },
        //                     LeftSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 2,
        //                         StringTokenLiteral = "2",
        //                         StringValueLiteral = "2",
        //                     },
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Multiplication,
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 / 2;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 1,
        //                         StringTokenLiteral = "1",
        //                         StringValueLiteral = "1",
        //                     },
        //                     LeftSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 2,
        //                         StringTokenLiteral = "2",
        //                         StringValueLiteral = "2",
        //                     },
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Division,
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 + 2;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 1,
        //                         StringTokenLiteral = "1",
        //                         StringValueLiteral = "1",
        //                     },
        //                     LeftSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 2,
        //                         StringTokenLiteral = "2",
        //                         StringValueLiteral = "2",
        //                     },
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Addition,
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 - 2;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 1,
        //                         StringTokenLiteral = "1",
        //                         StringValueLiteral = "1",
        //                     },
        //                     LeftSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 2,
        //                         StringTokenLiteral = "2",
        //                         StringValueLiteral = "2",
        //                     },
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Subtraction,
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 + 2 + 3;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Addition,
        //                     LeftSideNode = new InFixExpressionNode {
        //                         TokenTypeStringLiteral = TokenTypes.Sign_Addition,
        //                         LeftSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 1,
        //                             StringTokenLiteral = "1",
        //                             StringValueLiteral = "1",
        //                         },
        //                         RightSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 2,
        //                             StringTokenLiteral = "2",
        //                             StringValueLiteral = "2",
        //                         },
        //                     },
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 3,
        //                         StringTokenLiteral = "3",
        //                         StringValueLiteral = "3",
        //                     },
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 - 2 - 3;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Subtraction,
        //                     LeftSideNode = new InFixExpressionNode {
        //                         TokenTypeStringLiteral = TokenTypes.Sign_Subtraction,
        //                         LeftSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 1,
        //                             StringTokenLiteral = "1",
        //                             StringValueLiteral = "1",
        //                         },
        //                         RightSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 2,
        //                             StringTokenLiteral = "2",
        //                             StringValueLiteral = "2",
        //                         },
        //                     },
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 3,
        //                         StringTokenLiteral = "3",
        //                         StringValueLiteral = "3",
        //                     },
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 + 2 * 3;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     ExpressionNodeType = ExpressionNodeType.Infix,
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Addition,
        //                     LeftSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 1,
        //                         ExpressionNodeType = ExpressionNodeType.LiteralValue,
        //                         StringTokenLiteral = "1",
        //                         StringValueLiteral = "1",
        //                     },
        //                     RightSideNode = new InFixExpressionNode {
        //                         ExpressionNodeType = ExpressionNodeType.Infix,
        //                         TokenTypeStringLiteral = TokenTypes.Sign_Multiplication,
        //                         LeftSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 2,
        //                             ExpressionNodeType = ExpressionNodeType.LiteralValue,
        //                             StringTokenLiteral = "2",
        //                             StringValueLiteral = "2",
        //                         },
        //                         RightSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 3,
        //                             ExpressionNodeType = ExpressionNodeType.LiteralValue,
        //                             StringTokenLiteral = "3",
        //                             StringValueLiteral = "3",
        //                         },
        //                     },
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = (1 + 2) * 3;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Multiplication,
        //                     LeftSideNode = new InFixExpressionNode {
        //                         TokenTypeStringLiteral = TokenTypes.Sign_Addition,
        //                         LeftSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 1,
        //                             StringTokenLiteral = "1",
        //                             StringValueLiteral = "1",
        //                         },
        //                         RightSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 2,
        //                             StringTokenLiteral = "2",
        //                             StringValueLiteral = "2",
        //                         },
        //                     },
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 3,
        //                         StringTokenLiteral = "3",
        //                         StringValueLiteral = "3",
        //                     },
        //                 },
        //             },
        //         },
        //     },
        // ];
        // yield return [
        //     "Integer integerIdentifier = 1 - 2 * 3;",
        //     new PotatoRootAstNode {
        //         VariableAssignments = {
        //             new IntegerAssignmentStatementNode {
        //                 VariableLiteral = "integerIdentifier",
        //                 VariableExpressionNode = new InFixExpressionNode {
        //                     TokenTypeStringLiteral = TokenTypes.Sign_Multiplication,
        //                     LeftSideNode = new InFixExpressionNode {
        //                         TokenTypeStringLiteral = TokenTypes.Sign_Subtraction,
        //                         LeftSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 1,
        //                             StringTokenLiteral = "1",
        //                             StringValueLiteral = "1",
        //                         },
        //                         RightSideNode = new IntegerLiteralExpressionNode {
        //                             Value = 2,
        //                             StringTokenLiteral = "2",
        //                             StringValueLiteral = "2",
        //                         },
        //                     },
        //                     RightSideNode = new IntegerLiteralExpressionNode {
        //                         Value = 3,
        //                         StringTokenLiteral = "3",
        //                         StringValueLiteral = "3",
        //                     },
        //                 },
        //             },
        //         },
        //     },
        // ];
        yield return [
            "Integer integerIdentifier = 1 - 2 / 3;",
            new PotatoRootAstNode {
                VariableAssignments = {
                    new IntegerAssignmentStatementNode {
                        VariableLiteral = "integerIdentifier",
                        VariableExpressionNode = new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Subtraction,
                            LeftSideNode = new InFixExpressionNode {
                                TokenType = TokenTypesEnum.Sign_Division,
                                LeftSideNode = new IntegerLiteralExpressionNode {
                                    Value = 1,
                                    StringTokenLiteral = "2",
                                    StringValueLiteral = "2",
                                },
                                RightSideNode = new IntegerLiteralExpressionNode {
                                    Value = 2,
                                    StringTokenLiteral = "3",
                                    StringValueLiteral = "3",
                                },
                            },
                            RightSideNode = new IntegerLiteralExpressionNode {
                                Value = 3,
                                StringTokenLiteral = "1",
                                StringValueLiteral = "1",
                            },
                        },
                    },
                },
            },
        ];
        yield return [
            "Integer integerIdentifier = (1 - 2) / 3;",
            new PotatoRootAstNode {
                VariableAssignments = {
                    new IntegerAssignmentStatementNode {
                        VariableLiteral = "integerIdentifier",
                        VariableExpressionNode = new InFixExpressionNode {
                            TokenType = TokenTypesEnum.Sign_Division,
                            LeftSideNode = new InFixExpressionNode {
                                TokenType = TokenTypesEnum.Sign_Subtraction,
                                LeftSideNode = new IntegerLiteralExpressionNode {
                                    Value = 1,
                                    StringTokenLiteral = "1",
                                    StringValueLiteral = "1",
                                },
                                RightSideNode = new IntegerLiteralExpressionNode {
                                    Value = 2,
                                    StringTokenLiteral = "2",
                                    StringValueLiteral = "2",
                                },
                            },
                            RightSideNode = new IntegerLiteralExpressionNode {
                                Value = 3,
                                StringTokenLiteral = "3",
                                StringValueLiteral = "3",
                            },
                        },
                    },
                },
            },
        ];
    }

    [Theory]
    [MemberData(nameof(IntegerAssignmentCasesTestData))]
    public void IntegerAssignment(string input, PotatoRootAstNode expectedResult)
    {
        IEnumerable<string> testData = ReadTestData(input);
        PotatoRootAstNode result = Parser.Parse(testData);
        CheckResult(result, expectedResult);
        PrintResult(result);
    }
}
