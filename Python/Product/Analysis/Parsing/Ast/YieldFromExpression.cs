using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {

    // New in Pep380 for Python 3.3. Yield From is an expression with a return value.
    //    x = yield from z
    // The return value (x) is taken from the value attribute of a StopIteration
    // error raised by next(z) or z.send().
    public class YieldFromExpression : Expression {
        private readonly Expression _expression;

        public YieldFromExpression(Expression expression) {
            _expression = expression;
        }

        public Expression Expression {
            get { return _expression; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_expression != null) {
                    _expression.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override string CheckAugmentedAssign() {
            return CheckAssign();
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            res.Append(this.GetProceedingWhiteSpace(ast));
            res.Append("yield");
            res.Append(this.GetSecondWhiteSpace(ast));
            res.Append("from");
            if (!this.IsAltForm(ast)) {
                Expression.AppendCodeString(res, ast, format);
                var itemWhiteSpace = this.GetListWhiteSpace(ast);
                if (itemWhiteSpace != null) {
                    res.Append(",");
                    res.Append(itemWhiteSpace[0]);
                }
            }
        }
    }
}
