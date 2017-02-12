using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class StarredExpression : Expression {
        private readonly Expression _expr;

        public StarredExpression(Expression expr) {
            _expr = expr;
        }

        public Expression Expression {
            get {
                return _expr;
            }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                _expr.Walk(walker);
            }
        }

        internal override string CheckAssign() {
            return null;
        }

        internal override string CheckAugmentedAssign() {
            return "invalid syntax";
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            res.Append(this.GetProceedingWhiteSpace(ast));
            res.Append('*');
            _expr.AppendCodeString(res, ast, format);
        }
    }
}
