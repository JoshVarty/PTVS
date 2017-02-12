using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class AugmentedAssignStatement : Statement {
        private readonly PythonOperator _op;
        private readonly Expression _left;
        private readonly Expression _right;

        public AugmentedAssignStatement(PythonOperator op, Expression left, Expression right) {
            _op = op;
            _left = left; 
            _right = right;
        }

        public PythonOperator Operator {
            get { return _op; }
        }

        public Expression Left {
            get { return _left; }
        }

        public Expression Right {
            get { return _right; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_left != null) {
                    _left.Walk(walker);
                }
                if (_right != null) {
                    _right.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            _left.AppendCodeString(res, ast, format);
            res.Append(this.GetProceedingWhiteSpace(ast));
            res.Append(_op.ToCodeString());
            res.Append('=');
            _right.AppendCodeString(res, ast, format);
        }

        public override string GetLeadingWhiteSpace(PythonAst ast) {
            return _left.GetLeadingWhiteSpace(ast);
        }

        public override void SetLeadingWhiteSpace(PythonAst ast, string whiteSpace) {
            _left.SetLeadingWhiteSpace(ast, whiteSpace);
        }
    }
}
