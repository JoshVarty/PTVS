using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Tracks a variable that came out of analysis.  Includes a location (file, line,
    /// column) as well as a variable type (definition, reference, or value).  
    /// 
    /// Used for find all references, goto def, etc...
    /// 
    /// You can get ahold of these by calling snapshot.AnalyzeExpression(...) which
    /// is an extension method defined in <see cref="Microsoft.PythonTools.Intellisense.PythonAnalysisExtensions"/>
    /// </summary>
    public sealed class AnalysisVariable {
        private readonly AnalysisLocation _loc;
        private readonly VariableType _type;

        public AnalysisVariable(VariableType type, AnalysisLocation location) {
            _loc = location;
            _type = type;
        }

        public AnalysisLocation Location {
            get { return _loc; }
        }

        public VariableType Type {
            get { return _type; }
        }
    }
}
