using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {

    public class ContinueStatement : Statement {        
        public ContinueStatement() {
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            format.ReflowComment(res, this.GetProceedingWhiteSpace(ast));
            res.Append("continue");
        }
    }
}
