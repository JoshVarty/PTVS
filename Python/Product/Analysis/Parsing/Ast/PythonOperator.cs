namespace Microsoft.PythonTools.Parsing {
    public enum PythonOperator {
        None,

        // Unary
        Not,
        Pos,
        Invert,
        Negate,

        // Binary

        Add,
        Subtract,
        Multiply,
        Divide,
        TrueDivide,
        Mod,
        BitwiseAnd,
        BitwiseOr,
        Xor,
        LeftShift,
        RightShift,
        Power,
        FloorDivide,

        // Comparisons

        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Equal,
        NotEqual,
        In,
        NotIn,
        IsNot,
        Is,

        // Matrix Multiplication (new in 2.2)
        MatMultiply,

        // Aliases
        ExclusiveOr = Xor,
        Equals = Equal,
        NotEquals = NotEqual,
    }

    internal static class PythonOperatorExtensions {
        internal static string ToCodeString(this PythonOperator self) {
            switch (self) {
                case PythonOperator.Not: return "not";
                case PythonOperator.Pos: return "+";
                case PythonOperator.Invert: return "~";
                case PythonOperator.Negate: return "-";
                case PythonOperator.Add: return "+";
                case PythonOperator.Subtract: return "-";
                case PythonOperator.Multiply: return "*";
                case PythonOperator.MatMultiply: return "@";
                case PythonOperator.Divide: return "/";
                case PythonOperator.TrueDivide: return "/";
                case PythonOperator.Mod: return "%";
                case PythonOperator.BitwiseAnd: return "&";
                case PythonOperator.BitwiseOr: return "|";
                case PythonOperator.Xor: return "^";
                case PythonOperator.LeftShift: return "<<";
                case PythonOperator.RightShift: return ">>";
                case PythonOperator.Power: return "**";
                case PythonOperator.FloorDivide: return "//";
                case PythonOperator.LessThan: return "<";
                case PythonOperator.LessThanOrEqual: return "<=";
                case PythonOperator.GreaterThan: return ">";
                case PythonOperator.GreaterThanOrEqual: return ">=";
                case PythonOperator.Equal: return "==";
                case PythonOperator.NotEqual: return "!=";
                case PythonOperator.In: return "in";
                case PythonOperator.NotIn: return "not in";
                case PythonOperator.IsNot: return "is not";
                case PythonOperator.Is: return "is";
            }
            return "";
        }
    }
}
