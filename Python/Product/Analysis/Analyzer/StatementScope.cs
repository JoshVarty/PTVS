using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Analyzer {
    class StatementScope : InterpreterScope {
        public int _startIndex, _endIndex;

        public StatementScope(int index, InterpreterScope outerScope)
            : base(null, outerScope) {
            _startIndex = _endIndex = index;
        }

        public override string Name {
            get { return "<statements>"; }
        }

        public override int GetStart(PythonAst ast) {
            return ast.IndexToLocation(_startIndex).Index;
        }

        public override int GetStop(PythonAst ast) {
            return ast.IndexToLocation(_endIndex).Index;
        }

        public int EndIndex {
            set {
                _endIndex = value;
            }
        }

        // Forward variable handling to the outer scope.

        public override VariableDef CreateVariable(Node node, AnalysisUnit unit, string name, bool addRef = true) {
            return OuterScope.CreateVariable(node, unit, name, addRef);
        }

        public override VariableDef AddVariable(string name, VariableDef variable = null) {
            return OuterScope.AddVariable(name, variable);
        }

        internal override bool RemoveVariable(string name) {
            return OuterScope.RemoveVariable(name);
        }

        internal override void ClearVariables() {
            OuterScope.ClearVariables();
        }
    }
}
