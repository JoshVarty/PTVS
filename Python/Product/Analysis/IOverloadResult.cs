using System.Diagnostics.CodeAnalysis;

namespace Microsoft.PythonTools.Analysis {
    public interface IOverloadResult {
        string Name { get; }
        string Documentation { get; }
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays",
            Justification = "breaking change")]
        ParameterResult[] Parameters { get; }
    }
}
