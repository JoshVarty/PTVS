using System.IO;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Debugger {
    class PythonModule {
        private readonly int _moduleId;
        private readonly string _filename;

        public PythonModule(int moduleId, string filename) {
            _moduleId = moduleId;
            _filename = filename;
        }

        public int ModuleId {
            get {
                return _moduleId;
            }
        }

        public string Name {
            get {
                
                if (PathUtils.IsValidPath(_filename)) {
                    return Path.GetFileNameWithoutExtension(_filename);
                }
                return _filename;
            }
        }

        public string Filename {
            get {
                return _filename;
            }
        }
    }
}
