using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis {
    public sealed class VariablesResult : IEnumerable<IAnalysisVariable> {
        private readonly IEnumerable<IAnalysisVariable> _vars;
        private readonly PythonAst _ast;

        internal VariablesResult(IEnumerable<IAnalysisVariable> variables, PythonAst expr) {
            _vars = variables;
            _ast = expr;
        }

        public IEnumerator<IAnalysisVariable> GetEnumerator() {
            return _vars.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _vars.GetEnumerator();
        }

        public PythonAst Ast {
            get {
                return _ast;
            }
        }
    }
}
