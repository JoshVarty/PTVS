using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class NameExpression : Expression {
        public static readonly NameExpression[] EmptyArray = new NameExpression[0];
        public static readonly NameExpression Empty = new NameExpression("");

        private readonly string _name;

        public NameExpression(string name) {
            _name = name ?? "";
        }

        public string/*!*/ Name {
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
            }
            walker.PostWalk(this);
        }

        public PythonReference GetVariableReference(PythonAst ast) {
            return GetVariableReference(this, ast);
        }

        public void AddPreceedingWhiteSpace(PythonAst ast, string whiteSpace) {
            ast.SetAttribute(this, NodeAttributes.PreceedingWhiteSpace, whiteSpace);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpaceDefaultNull(ast));
            if (format.UseVerbatimImage) {
                res.Append(this.GetVerbatimImage(ast) ?? _name);
            } else {
                res.Append(_name);
            }
        }
    }
}
