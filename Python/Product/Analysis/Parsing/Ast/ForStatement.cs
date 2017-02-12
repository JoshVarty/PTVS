using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class ForStatement : Statement {
        private int _headerIndex, _elseIndex;
        private readonly Expression _left;
        private Expression _list;
        private Statement _body;
        private readonly Statement _else;
        private readonly bool _isAsync;

        public ForStatement(Expression left, Expression list, Statement body, Statement else_) {
            _left = left;
            _list = list;
            _body = body;
            _else = else_;
        }

        public ForStatement(Expression left, Expression list, Statement body, Statement else_, bool isAsync)
            : this(left, list, body, else_) {
            _isAsync = isAsync;
        }

        public int HeaderIndex {
            get { return _headerIndex;  }
            set { _headerIndex = value; }
        }

        public int ElseIndex {
            get { return _elseIndex; }
            set { _elseIndex = value; }
        }

        public Expression Left {
            get { return _left; }
        }

        public Statement Body {
            get { return _body; }
            set { _body = value; }
        }

        public Expression List {
            get { return _list; }
            set { _list = value; }
        }

        public Statement Else {
            get { return _else; }
        }

        public bool IsAsync {
            get { return _isAsync; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_left != null) {
                    _left.Walk(walker);
                }
                if (_list != null) {
                    _list.Walk(walker);
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
            if (IsAsync) {
                res.Append("async");
                res.Append(this.GetFourthWhiteSpace(ast));
            }
            res.Append("for");
            _left.AppendCodeString(res, ast, format);
            if (!this.IsIncompleteNode(ast)) {
                res.Append(this.GetSecondWhiteSpace(ast));
                res.Append("in");
                _list.AppendCodeString(res, ast, format);
                _body.AppendCodeString(res, ast, format);   // colon is handled by suite statements...
                if (_else != null) {
                    format.ReflowComment(res, this.GetThirdWhiteSpace(ast));
                    res.Append("else");
                    _else.AppendCodeString(res, ast, format);
                }
            }
        }
    }
}
