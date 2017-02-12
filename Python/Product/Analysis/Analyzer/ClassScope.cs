using Microsoft.PythonTools.Analysis.Values;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Analyzer {
    sealed class ClassScope : InterpreterScope {
        public ClassScope(ClassInfo classInfo, ClassDefinition ast, InterpreterScope outerScope)
            : base(classInfo, ast, outerScope) {
            classInfo.Scope = this;
        }

        public ClassInfo Class {
            get {
                return (ClassInfo)AnalysisValue;
            }
        }

        public override int GetBodyStart(PythonAst ast) {
            return ast.IndexToLocation(((ClassDefinition)Node).HeaderIndex).Index;
        }

        public override string Name {
            get { return Class.ClassDefinition.Name; }
        }

        public override bool VisibleToChildren {
            get {
                return false;
            }
        }

        public override bool AssignVariable(string name, Node location, AnalysisUnit unit, IAnalysisSet values) {
            var res = base.AssignVariable(name, location, unit, values);

            if (name == "__metaclass__") {
                // assignment to __metaclass__, save it in our metaclass variable
                Class.GetOrCreateMetaclassVariable().AddTypes(unit, values);
            }

            return res;
        }
    }
}
