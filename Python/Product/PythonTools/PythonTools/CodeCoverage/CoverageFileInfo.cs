using System.Collections.Generic;

namespace Microsoft.PythonTools.CodeCoverage {
    class CoverageFileInfo {
        public readonly string Filename;
        public readonly HashSet<int> Hits;

        public CoverageFileInfo(string filename, HashSet<int> hits) {
            Filename = filename;
            Hits = hits;
        }
    }
}
