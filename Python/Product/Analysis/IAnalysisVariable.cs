namespace Microsoft.PythonTools.Analysis {
    public interface IAnalysisVariable {
        /// <summary>
        /// Returns the location of where the variable is defined.
        /// </summary>
        LocationInfo Location {
            get;
        }

        VariableType Type {
            get;
        }
    }
}
