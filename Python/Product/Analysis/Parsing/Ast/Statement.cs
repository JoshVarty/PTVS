using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public abstract class Statement : Node {
        internal Statement() {
        }

        public virtual string Documentation {
            get {
                return null;
            }
        }

        internal override sealed void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            AppendCodeStringStmt(res, ast, format);
        }

        internal abstract void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format);

        /// <summary>
        /// Returns the expression contained by the statement.
        /// 
        /// Returns null if it's not an expression statement or return statement.
        /// 
        /// New in 1.1.
        /// </summary>
        public static Expression GetExpression(Statement statement) {
            if (statement is ExpressionStatement) {
                return ((ExpressionStatement)statement).Expression;
            } else if (statement is ReturnStatement) {
                return ((ReturnStatement)statement).Expression;
            } else {
                return null;
            }
        }
    }
}
