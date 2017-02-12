using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class ErrorStatement : Statement {
        private readonly Statement[] _preceeding;

        public ErrorStatement(Statement[] preceeding) {
            _preceeding = preceeding;
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            foreach(var preceeding in _preceeding) {
                preceeding.AppendCodeString(res, ast, format);
            }
            res.Append(this.GetVerbatimImage(ast) ?? "<error stmt>");
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                foreach (var preceeding in _preceeding) {
                    preceeding.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }
    }
}
