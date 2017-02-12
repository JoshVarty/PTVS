using System.Runtime.InteropServices;

namespace Microsoft.PythonTools.Profiling {
    [ComVisible(true)]
    public sealed class ReportWrapper : IPythonPerformanceReport {
        private readonly Report _report;

        internal ReportWrapper(Report report) {
            _report = report;
        }

        #region IPythonPerformanceReport Members

        public string Filename {
            get { return _report.Filename; }
        }

        #endregion
    }
}
