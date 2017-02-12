using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public sealed class Arg : Node {
        private readonly Expression _name;
        private readonly Expression _expression;

        public Arg(Expression expression) : this(null, expression) { }

        public Arg(Expression name, Expression expression) {
            _name = name;
            _expression = expression;
        }

        public string Name {
            get {
                var nameExpr = _name as NameExpression;
                if (nameExpr != null) {
                    return nameExpr.Name;
                }
                return null;
            }
        }

        public Expression NameExpression {
            get {
                return _name;
            }
        }

        public Expression Expression {
            get { return _expression; }
        } 

        public override string ToString() {
            return base.ToString() + ":" + _name;
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_expression != null) {
                    _expression.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format)
        {
            if (_name != null) {
                if (Name == "*" || Name == "**") {
                    _name.AppendCodeString(res, ast, format);
                    _expression.AppendCodeString(res, ast, format);
                } else {
                    // keyword arg
                    _name.AppendCodeString(res, ast, format);
                    res.Append(this.GetProceedingWhiteSpace(ast));
                    res.Append('=');
                    _expression.AppendCodeString(res, ast, format);
                }
            } else {
                _expression.AppendCodeString(res, ast, format);
            }
        }
    }
}
