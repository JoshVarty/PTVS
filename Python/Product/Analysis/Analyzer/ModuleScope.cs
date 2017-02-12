using Microsoft.PythonTools.Analysis.Values;

namespace Microsoft.PythonTools.Analysis.Analyzer {
    sealed class ModuleScope : InterpreterScope {

        public ModuleScope(ModuleInfo moduleInfo)
            : base(moduleInfo, null) {
        }

        private ModuleScope(ModuleScope scope)
            : base(scope.AnalysisValue, scope, true) {
        }

        public ModuleInfo Module { get { return AnalysisValue as ModuleInfo; } }

        public override string Name {
            get { return Module.Name; }
        }

        public override bool AssignVariable(string name, Parsing.Ast.Node location, AnalysisUnit unit, IAnalysisSet values) {
            if (base.AssignVariable(name, location, unit, values)) {
                Module.ModuleDefinition.EnqueueDependents();
                return true;
            }

            return false;
        }

        public ModuleScope CloneForPublish() {
            return new ModuleScope(this);
        }
    }
}
