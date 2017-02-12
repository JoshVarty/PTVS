using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class ReturnStatement : Statement {
        private readonly Expression _expression;

        public ReturnStatement(Expression expression) {
            _expression = expression;
        }

        public Expression Expression {
            get { return _expression; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_expression != null) {
                    _expression.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        public void RoundTripRemoveValueWhiteSpace(PythonAst ast) {
            ast.SetAttribute(this, NodeAttributes.IsAltFormValue, NodeAttributes.IsAltFormValue);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append("return");
            if (_expression != null) {
                int len = res.Length;

                _expression.AppendCodeString(res, ast, format);
                if (this.IsAltForm(ast)) {
                    // remove the leading white space and insert a single space
                    res.Remove(len, _expression.GetLeadingWhiteSpace(ast).Length);
                    res.Insert(len, ' ');
                }
            }
        }
    }
}
