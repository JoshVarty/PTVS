using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class BackQuoteExpression : Expression {
        private readonly Expression _expression;

        public BackQuoteExpression(Expression expression) {
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

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append('`');
            _expression.AppendCodeString(res, ast, format);
            if (!this.IsMissingCloseGrouping(ast)) {
                res.Append(this.GetSecondWhiteSpace(ast));
                res.Append('`');
            }
        }
    }
}
