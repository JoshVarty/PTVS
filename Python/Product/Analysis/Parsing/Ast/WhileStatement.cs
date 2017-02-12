using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class WhileStatement : Statement {
        // Marks the end of the condition of the while loop
        private int _indexHeader, _elseIndex;
        private readonly Expression _test;
        private readonly Statement _body;
        private readonly Statement _else;

        public WhileStatement(Expression test, Statement body, Statement else_) {
            _test = test;
            _body = body;
            _else = else_;
        }

        public Expression Test {
            get { return _test; }
        }

        public Statement Body {
            get { return _body; }
        }

        public Statement ElseStatement {
            get { return _else; }
        }

        public int HeaderIndex {
            get {
                return _indexHeader;
            }
        }

        public int ElseIndex {
            get {
                return _elseIndex;
            }
        }

        public void SetLoc(int start, int header, int end, int elseIndex) {
            SetLoc(start, end);
            _indexHeader = header;
            _elseIndex = elseIndex;
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_test != null) {
                    _test.Walk(walker);
                }
                if (_body != null) {
                    _body.Walk(walker);
                }
                if (_else != null) {
                    _else.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append("while");
            _test.AppendCodeString(res, ast, format);
            _body.AppendCodeString(res, ast, format);
            if (_else != null) {
                format.ReflowComment(res, this.GetSecondWhiteSpaceDefaultNull(ast));
                res.Append("else");
                _else.AppendCodeString(res, ast, format);
            }
        }
    }
}
