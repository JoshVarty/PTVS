using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class AssertStatement : Statement {
        private readonly Expression _test, _message;

        public AssertStatement(Expression test, Expression message) {
            _test = test;
            _message = message;
        }

        public Expression Test {
            get { return _test; }
        }

        public Expression Message {
            get { return _message; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_test != null) {
                    _test.Walk(walker);
                }
                if (_message != null) {
                    _message.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append("assert");
            _test.AppendCodeString(res, ast, format);
            if (_message != null) {
                res.Append(this.GetSecondWhiteSpace(ast));
                res.Append(',');
                _message.AppendCodeString(res, ast, format);
            }
        }
    }
}
