using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public sealed class MemberExpression : Expression {
        private readonly Expression _target;
        private readonly string _name;
        private int _nameHeader;

        public MemberExpression(Expression target, string name) {
            _target = target;
            _name = name;
        }

        public void SetLoc(int start, int name, int end) {
            SetLoc(start, end);
            _nameHeader = name;
        }

        /// <summary>
        /// Returns the index which is the start of the name.
        /// </summary>
        public int NameHeader {
            get {
                return _nameHeader;
            }
        }

        public Expression Target {
            get { return _target; }
        }

        public string Name {
            get { return _name; }
        }

        public override string ToString() {
            return base.ToString() + ":" + _name;
        }

        internal override string CheckAssign() {
            return null;
        }

        internal override string CheckDelete() {
            return null;
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_target != null) {
                    _target.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            _target.AppendCodeString(res, ast, format);
            res.Append(this.GetProceedingWhiteSpaceDefaultNull(ast));
            res.Append('.');
            if (!this.IsIncompleteNode(ast)) {
                res.Append(this.GetSecondWhiteSpaceDefaultNull(ast));
                if (format.UseVerbatimImage) {
                    res.Append(this.GetVerbatimImage(ast) ?? _name);
                } else {
                    res.Append(_name);
                }
            }
        }

        public override string GetLeadingWhiteSpace(PythonAst ast) {
            return _target.GetLeadingWhiteSpace(ast);
        }

        public override void SetLeadingWhiteSpace(PythonAst ast, string whiteSpace) {
            _target.SetLeadingWhiteSpace(ast, whiteSpace);
        }

        /// <summary>
        /// Returns the span of the name component of the expression
        /// </summary>
        public SourceSpan GetNameSpan(PythonAst parent) {
            return new SourceSpan(parent.IndexToLocation(_nameHeader), GetEnd(parent));
        }
    }
}
