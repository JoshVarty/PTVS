using Microsoft.PythonTools.Analysis.Values;

namespace Microsoft.PythonTools.Analysis {
    static class ProjectEntryExtensions {
        public static ModuleInfo GetModuleInfo(this IPythonProjectEntry entry) {
            var pe = entry as ProjectEntry;
            return pe != null ? pe.MyScope : null;
        }
    }
}
