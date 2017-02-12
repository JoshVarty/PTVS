
namespace Microsoft.PythonTools.CodeCoverage {
    class CoverageHit {
        /// <summary>
        /// The 1 based line number
        /// </summary>
        public readonly int LineNumber;
        /// <summary>
        /// The starting line of the function (optional, used to disambiguate function)
        /// </summary>
        public readonly int? FunctionStart;
    }
}
