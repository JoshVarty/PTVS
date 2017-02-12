using System;
using Microsoft.PythonTools.Intellisense;

namespace Microsoft.PythonTools.Project {
    /// <summary>
    /// Data for the <see cref="IPythonProject2.ProjectAnalyzerChanging"/> event
    /// specifying the previous and new analyzer.
    /// </summary>
    public sealed class AnalyzerChangingEventArgs : EventArgs {
        private readonly VsProjectAnalyzer _old, _new;

        /// <summary>
        /// The previous analyzer, if any.
        /// </summary>
        public VsProjectAnalyzer Old { get { return _old; } }

        /// <summary>
        /// The new analyzer, if any.
        /// </summary>
        public VsProjectAnalyzer New { get { return _new; } }

        public AnalyzerChangingEventArgs(VsProjectAnalyzer oldAnalyzer, VsProjectAnalyzer newAnalyzer) {
            _old = oldAnalyzer;
            _new = newAnalyzer;
        }
    }
}
