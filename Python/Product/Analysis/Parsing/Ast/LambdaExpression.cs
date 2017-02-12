using System.Diagnostics;
using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class LambdaExpression : Expression {
        private readonly FunctionDefinition _function;

        public LambdaExpression(FunctionDefinition function) {
            _function = function;
        }

        public FunctionDefinition Function {
            get { return _function; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_function != null) {
                    _function.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append("lambda");
            var commaWhiteSpace = this.GetListWhiteSpace(ast);

            _function.ParamsToString(res, ast, commaWhiteSpace, format);
            string namedOnlyText = this.GetExtraVerbatimText(ast);
            if (namedOnlyText != null) {
                res.Append(namedOnlyText);
            }
            if (!this.IsIncompleteNode(ast)) {
                res.Append(this.GetSecondWhiteSpace(ast));
                res.Append(":");
                if (_function.Body is ReturnStatement) {
                    ((ReturnStatement)_function.Body).Expression.AppendCodeString(res, ast, format);
                } else {
                    Debug.Assert(_function.Body is ExpressionStatement);
                    ((ExpressionStatement)_function.Body).Expression.AppendCodeString(res, ast, format);
                }
            }
        }
    }
}
