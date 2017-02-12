using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class ErrorExpression : Expression {
        private readonly Expression _preceding;
        private readonly string _verbatimImage;
        private readonly ErrorExpression _nested;

        private ErrorExpression(string verbatimImage, Expression preceding, ErrorExpression nested) {
            _preceding = preceding;
            _verbatimImage = verbatimImage;
            _nested = nested;
        }

        public ErrorExpression(string verbatimImage, Expression preceding) : this(verbatimImage, preceding, null) { }

        public ErrorExpression AddPrefix(string verbatimImage, Expression preceding) {
            return new ErrorExpression(verbatimImage, preceding, this);
        }

        public string VerbatimImage => _verbatimImage;


        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            _preceding?.AppendCodeString(res, ast, format);
            res.Append(_verbatimImage ?? "<error>");
            _nested?.AppendCodeString(res, ast, format);
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                _preceding?.Walk(walker);
                _nested?.Walk(walker);
            }
            walker.PostWalk(this);
        }
    }
}
