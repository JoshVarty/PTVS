using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Analysis.Values;

namespace Microsoft.PythonTools.Analysis {
    class ModuleReference {
        public IModule Module;

        private readonly Lazy<HashSet<ModuleInfo>> _references = new Lazy<HashSet<ModuleInfo>>();

        public ModuleReference(IModule module = null) {
            Module = module;
        }

        public AnalysisValue AnalysisModule {
            get {
                return Module as AnalysisValue;
            }
        }

        public bool AddReference(ModuleInfo module) {
            return _references.Value.Add(module);
        }

        public bool RemoveReference(ModuleInfo module) {
            return _references.IsValueCreated && _references.Value.Remove(module);
        }

        public bool HasReferences {
            get {
                return _references.IsValueCreated && _references.Value.Any();
            }
        }

        public IEnumerable<ModuleInfo> References {
            get {
                return _references.IsValueCreated ? _references.Value : Enumerable.Empty<ModuleInfo>();
            }
        }
    }
}
