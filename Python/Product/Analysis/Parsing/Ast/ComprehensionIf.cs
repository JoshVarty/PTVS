using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class ComprehensionIf : ComprehensionIterator {
        private readonly Expression _test;
        private int _headerIndex;

        public ComprehensionIf(Expression test) {
            _test = test;
        }

        public Expression Test {
            get { return _test; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_test != null) {
                    _test.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            res.Append(this.GetProceedingWhiteSpace(ast));
            res.Append("if");
            _test.AppendCodeString(res, ast, format);
        }

        public int HeaderIndex { get { return _headerIndex; } set { _headerIndex = value; } }
    }
}
