using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class OrExpression : Expression {
        private readonly Expression _left, _right;

        public OrExpression(Expression left, Expression right) {
            _left = left;
            _right = right;
            StartIndex = left.StartIndex;
            EndIndex = right.EndIndex;
        }

        public Expression Left {
            get { return _left; }
        }

        public Expression Right {
            get { return _right; }
        }

        public override string NodeName {
            get {
                return "or expression";
            }
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

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            BinaryExpression.BinaryToCodeString(res, ast, format, this, _left, _right, "or");
        }

        public override string GetLeadingWhiteSpace(PythonAst ast) {
            return _left.GetLeadingWhiteSpace(ast);
        }

        public override void SetLeadingWhiteSpace(PythonAst ast, string whiteSpace) {
            _left.SetLeadingWhiteSpace(ast, whiteSpace);
        }
    }
}
