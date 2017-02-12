
namespace Microsoft.PythonTools.Analysis {
    class AnalysisVariable : IAnalysisVariable {
        private readonly LocationInfo _loc;
        private readonly VariableType _type;

        public AnalysisVariable(VariableType type, LocationInfo location) {
            _loc = location;
            _type = type;
        }

        #region IAnalysisVariable Members

        public LocationInfo Location {
            get { return _loc; }
        }

        public VariableType Type {
            get { return _type; }
        }

        #endregion
    }

}
