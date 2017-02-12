using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {

    public class DecoratorStatement : Statement {
        private readonly Expression[] _decorators;

        public DecoratorStatement(Expression[] decorators) {
            _decorators = decorators;
        }

        public IList<Expression> Decorators {
            get { return _decorators; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                foreach (var decorator in _decorators) {
                    if (decorator != null) {
                        decorator.Walk(walker);
                    }
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            var decorateWhiteSpace = this.GetNamesWhiteSpace(ast);
            if (Decorators != null) {
                for (int i = 0, curWhiteSpace = 0; i < Decorators.Count; i++) {
                    if (decorateWhiteSpace != null) {
                        format.ReflowComment(res, decorateWhiteSpace[curWhiteSpace++]);
                    }
                    res.Append('@');
                    if (Decorators[i] != null) {
                        Decorators[i].AppendCodeString(res, ast, format);
                        if (decorateWhiteSpace != null) {
                            format.ReflowComment(res, decorateWhiteSpace[curWhiteSpace++]);
                        } else {
                            res.Append(Environment.NewLine);
                        }
                    }
                }
            }
        }

        public override string GetLeadingWhiteSpace(PythonAst ast) {
            var decorateWhiteSpace = this.GetNamesWhiteSpace(ast);
            if (decorateWhiteSpace != null && decorateWhiteSpace.Length > 0) {
                return decorateWhiteSpace[0];
            }
            return "";
        }

        public override void SetLeadingWhiteSpace(PythonAst ast, string whiteSpace) {
            var decorateWhiteSpace = this.GetNamesWhiteSpace(ast);
            if (decorateWhiteSpace != null && decorateWhiteSpace.Length > 0) {
                decorateWhiteSpace[0] = whiteSpace;
            }
            
        }
    }
}
