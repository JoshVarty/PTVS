using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    class ErrorParameter : Parameter {
        private readonly ErrorExpression _error;
        
        public ErrorParameter(ErrorExpression errorValue)
            : base("", ParameterKind.Normal) {
                _error = errorValue;
        }

        public ErrorExpression Error {
            get {
                return _error;
            }
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format, string leadingWhiteSpace) {
            string kwOnlyText = this.GetExtraVerbatimText(ast);
            if (kwOnlyText != null) {
                if (leadingWhiteSpace != null) {
                    res.Append(leadingWhiteSpace);
                    res.Append(kwOnlyText.TrimStart());
                    leadingWhiteSpace = null;
                } else {
                    res.Append(kwOnlyText);
                }
            }
            bool isAltForm = this.IsAltForm(ast);
            if (isAltForm) {
                res.Append(leadingWhiteSpace ?? this.GetProceedingWhiteSpace(ast));
                res.Append('(');
                leadingWhiteSpace = null;
            }
            _error.AppendCodeString(res, ast, format, leadingWhiteSpace);
            if (this.DefaultValue != null) {
                res.Append(this.GetSecondWhiteSpace(ast));
                res.Append('=');
                this.DefaultValue.AppendCodeString(res, ast, format);
            }
            if (isAltForm && !this.IsMissingCloseGrouping(ast)) {
                res.Append(this.GetSecondWhiteSpace(ast));
                res.Append(')');
            }
        }
    }
}
