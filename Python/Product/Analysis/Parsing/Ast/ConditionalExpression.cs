using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class ConditionalExpression : Expression {
        private readonly Expression _testExpr;
        private readonly Expression _trueExpr;
        private readonly Expression _falseExpr;

        public ConditionalExpression(Expression testExpression, Expression trueExpression, Expression falseExpression) {
            this._testExpr = testExpression;
            this._trueExpr = trueExpression;
            this._falseExpr = falseExpression;
        }

        public override string NodeName {
            get {
                return "conditional expression";
            }
        }

        public Expression FalseExpression {
            get { return _falseExpr; }
        }

        public Expression Test {
            get { return _testExpr; }
        }

        public Expression TrueExpression {
            get { return _trueExpr; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_testExpr != null) {
                    _testExpr.Walk(walker);
                }
                if (_trueExpr != null) {
                    _trueExpr.Walk(walker);
                }
                if (_falseExpr != null) {
                    _falseExpr.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            _trueExpr.AppendCodeString(res, ast, format);
            res.Append(this.GetProceedingWhiteSpace(ast));
            res.Append("if");
            if (!ast.HasVerbatim) {
                res.Append(' ');
            }
            _testExpr.AppendCodeString(res, ast, format);
            res.Append(this.GetSecondWhiteSpace(ast));
            if (!this.IsIncompleteNode(ast)) {
                res.Append("else");
                if (!ast.HasVerbatim) {
                    res.Append(' ');
                }
                _falseExpr.AppendCodeString(res, ast, format);
            }
        }

        public override string GetLeadingWhiteSpace(PythonAst ast) {
            return _trueExpr.GetLeadingWhiteSpace(ast);
        }

        public override void SetLeadingWhiteSpace(PythonAst ast, string whiteSpace) {
            _trueExpr.SetLeadingWhiteSpace(ast, whiteSpace);
        }
    }
}
