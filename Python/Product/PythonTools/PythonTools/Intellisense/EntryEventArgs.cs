using System;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Intellisense {
    class EntryEventArgs : EventArgs {
        public readonly AnalysisEntry Entry;

        public EntryEventArgs(AnalysisEntry entry) {
            Entry = entry;
        }
    }
}
