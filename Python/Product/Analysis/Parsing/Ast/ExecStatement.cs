using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    
    public class ExecStatement : Statement {
        private readonly Expression _code, _locals, _globals;

        public ExecStatement(Expression code, Expression locals, Expression globals) {
            _code = code;
            _locals = locals;
            _globals = globals;
        }

        public Expression Code {
            get { return _code; }
        }

        public Expression Locals {
            get { return _locals; }
        }

        public Expression Globals {
            get { return _globals; }
        }

        public bool NeedsLocalsDictionary() {
            return _globals == null && _locals == null;
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_code != null) {
                    _code.Walk(walker);
                }
                if (_locals != null) {
                    _locals.Walk(walker);
                }
                if (_globals != null) {
                    _globals.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append("exec");
            _code.AppendCodeString(res, ast, format);
            if (_globals != null) {
                res.Append(this.GetSecondWhiteSpace(ast));
                res.Append("in");
                _globals.AppendCodeString(res, ast, format);
                if (_locals != null) {
                    res.Append(this.GetThirdWhiteSpace(ast));
                    res.Append(',');
                    _locals.AppendCodeString(res, ast, format);
                }
            }
        }
    }
}
