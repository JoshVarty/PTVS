using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    internal class RangeInfo : BuiltinInstanceInfo {
        public RangeInfo(IPythonType seqType, PythonAnalyzer state)
            : base(state.ClassInfos[BuiltinTypeId.List]) {
        }

        public override IAnalysisSet GetEnumeratorTypes(Node node, AnalysisUnit unit) {
            return ProjectState.ClassInfos[BuiltinTypeId.Int].Instance;
        }

        public override IAnalysisSet GetIndex(Node node, AnalysisUnit unit, IAnalysisSet index) {
            // TODO: Return correct index value if we have a constant
            /*int? constIndex = SequenceInfo.GetConstantIndex(index);

            if (constIndex != null && constIndex.Value < _indexTypes.Count) {
                // TODO: Warn if outside known index and no appends?
                return _indexTypes[constIndex.Value];
            }*/

            return ProjectState.ClassInfos[BuiltinTypeId.Int].Instance;
        }
    }
}
