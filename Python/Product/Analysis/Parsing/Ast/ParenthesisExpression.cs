using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {

    public class ParenthesisExpression : Expression {
        private readonly Expression _expression;

        public ParenthesisExpression(Expression expression) {
            _expression = expression;
        }

        public Expression Expression {
            get { return _expression; }
        }

        internal override string CheckAssign() {
            return _expression.CheckAssign();
        }

        internal override string CheckDelete() {
            return _expression.CheckDelete();
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_expression != null) {
                    _expression.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append('(');
            
            _expression.AppendCodeString(
                res, 
                ast, 
                format,
                format.SpacesWithinParenthesisExpression != null ? format.SpacesWithinParenthesisExpression.Value ? " " : "" : null
            );
            if (!this.IsMissingCloseGrouping(ast)) {
                format.Append(
                    res,
                    format.SpacesWithinParenthesisExpression,
                    " ",
                    "",
                    this.GetSecondWhiteSpace(ast)
                );

                res.Append(')');
            }
        }
    }
}
