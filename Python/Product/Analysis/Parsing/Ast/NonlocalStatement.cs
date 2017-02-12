using System.Collections.Generic;
using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {

    public class NonlocalStatement : Statement {
        private readonly NameExpression[] _names;

        public NonlocalStatement(NameExpression[] names) {
            _names = names;
        }

        public IList<NameExpression> Names {
            get { return _names; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            var namesWhiteSpace = this.GetNamesWhiteSpace(ast);

            if (namesWhiteSpace != null) {
                ListExpression.AppendItems(res, ast, format, "nonlocal", "", this, Names.Count, (i, sb) => {
                    sb.Append(namesWhiteSpace[i]);
                    Names[i].AppendCodeString(res, ast, format);
                });
            } else {
                ListExpression.AppendItems(res, ast, format, "nonlocal", "", this, Names.Count, (i, sb) => Names[i].AppendCodeString(sb, ast, format));
            }
        }
    }
}
