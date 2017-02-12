using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class IfStatementTest : Node {
        private int _headerIndex;
        private readonly Expression _test;
        private Statement _body;

        public IfStatementTest(Expression test, Statement body) {
            _test = test;
            _body = body;
        }

        public int HeaderIndex {
            set { _headerIndex = value; }
            get { return _headerIndex; }
        }

        public Expression Test {
            get { return _test; }
        }

        public Statement Body {
            get { return _body; }
            set { _body = value; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_test != null) {
                    _test.Walk(walker);
                }
                if (_body != null) {
                    _body.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        public SourceLocation GetHeader(PythonAst ast) {
            return ast.IndexToLocation(_headerIndex);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            _test.AppendCodeString(res, ast, format);
            _body.AppendCodeString(res, ast, format);
        }
    }
}
