using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class DottedName : Node {
        private readonly NameExpression[] _names;

        public DottedName(NameExpression[] names) {
            _names = names;
        }

        public IList<NameExpression> Names {
            get { return _names; }
        }

        public virtual string MakeString() {
            if (_names.Length == 0) return String.Empty;

            StringBuilder ret = new StringBuilder(_names[0].Name);
            for (int i = 1; i < _names.Length; i++) {
                ret.Append('.');
                ret.Append(_names[i].Name);
            }
            return ret.ToString();
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                ;
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            var whitespace = this.GetNamesWhiteSpace(ast);
            
            for (int i = 0, whitespaceIndex = 0; i < _names.Length; i++) {
                if (whitespace != null) {
                    res.Append(whitespace[whitespaceIndex++]);
                }
                if (i != 0) {
                    res.Append('.');
                    if (whitespace != null) {
                        res.Append(whitespace[whitespaceIndex++]);
                    }
                }
                _names[i].AppendCodeString(res, ast, format);
            }
        }


        public override string GetLeadingWhiteSpace(PythonAst ast) {
            var whitespace = this.GetNamesWhiteSpace(ast);
            if (whitespace != null && whitespace.Length > 0) {
                return whitespace[0];
            }
            return null;
        }

        public override void SetLeadingWhiteSpace(PythonAst ast, string whiteSpace) {
            var whitespace = this.GetNamesWhiteSpace(ast);
            if (whitespace != null && whitespace.Length > 0) {
                whitespace[0] = whiteSpace;
            }
        }

    }
}
