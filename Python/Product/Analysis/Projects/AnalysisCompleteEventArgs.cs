using System;

namespace Microsoft.PythonTools.Projects {
    public sealed class AnalysisCompleteEventArgs : EventArgs {
        private readonly string _path;

        public string Path => _path;

        public AnalysisCompleteEventArgs(string path) {
            _path = path;
        }
    }
}
