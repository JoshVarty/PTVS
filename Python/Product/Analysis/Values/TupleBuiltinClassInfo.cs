using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    class TupleBuiltinClassInfo : SequenceBuiltinClassInfo {
        public TupleBuiltinClassInfo(IPythonType classObj, PythonAnalyzer projectState)
            : base(classObj, projectState) {
        }

        internal override SequenceInfo MakeFromIndexes(Node node, ProjectEntry entry) {
            if (_indexTypes.Count > 0) {
                var vals = new[] { new VariableDef() };
                vals[0].AddTypes(entry, _indexTypes, false, entry);
                return new SequenceInfo(vals, this, node, entry);
            } else {
                return new SequenceInfo(VariableDef.EmptyArray, this, node, entry);
            }
        }
    }
}
