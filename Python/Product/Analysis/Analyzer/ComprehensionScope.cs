using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Analyzer {
    sealed class ComprehensionScope : InterpreterScope {
        public ComprehensionScope(AnalysisValue comprehensionResult, Comprehension comprehension, InterpreterScope outerScope)
            : base(comprehensionResult, comprehension, outerScope) {
        }

        public override string Name {
            get { return "<comprehension scope>";  }
        }

        public override InterpreterScope AddNodeScope(Node node, InterpreterScope scope) {
            return OuterScope.AddNodeScope(node, scope);
        }

        internal override bool RemoveNodeScope(Node node) {
            return OuterScope.RemoveNodeScope(node);
        }

        internal override void ClearNodeScopes() {
            OuterScope.ClearNodeScopes();
        }

        public override IAnalysisSet AddNodeValue(Node node, NodeValueKind kind, IAnalysisSet variable) {
            return OuterScope.AddNodeValue(node, kind, variable);
        }

        internal override bool RemoveNodeValue(Node node) {
            return OuterScope.RemoveNodeValue(node);
        }

        internal override void ClearNodeValues() {
            OuterScope.ClearNodeValues();
        }
    }
}
