namespace Potato;

public struct TokenTypes
{
    public const string Keyword_Boolean_True = "true";
    public const string Keyword_Boolean_False = "false";
    public const string Keyword_String = "String";
    public const string Keyword_Integer = "Integer";
    public const string Keyword_Boolean = "Boolean";
    public const string Keyword_Double = "Double";
    public const string Space = " ";
    public const string Sign_Semicolon = ";";
    public const string Sign_DoubleQuote = "\"";
    public const string Sign_DoubleEquality = "==";
    public const string Sign_BangEquality = "!=";
    public const string Sign_Bang = "!";
    public const string Sign_OpenParentheses = "(";
    public const string Sign_CloseParentheses = ")";
    public const string Sign_Addition = "+";
    public const string Sign_Subtraction = "-";
    public const string Sign_Assignment = "=";
    public const string Sign_Multiplication = "*";
    public const string Sign_Division = "/";
    public const string Identifier = "identifier";

    /// <summary>
    ///     Represents the literal value of an integer.
    ///     <example>
    ///         <code>
    /// // the literal value here is 5
    /// Integer integerIdentifier = 5;
    /// </code>
    ///     </example>
    /// </summary>
    public const string IntegerLiteral = "value_integer";

    public const string Value_String = "value_string";
    public const string Value_Boolean = "value_boolean";
    public const string Value_Double = "value_double";
}
