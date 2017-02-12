
using System;

namespace Microsoft.PythonTools.Analysis {
    /// <summary>
    /// Provides information about a value exported from a module.
    /// </summary>
    public struct ExportedMemberInfo {
        private readonly string _fromName, _name;
        
        public ExportedMemberInfo(string fromName, string name) {
            _fromName = fromName;
            _name = name;
        }

        /// <summary>
        /// The name of the value being exported, fully qualified with the
        /// module/package name.
        /// </summary>
        public string Name {
            get {
                if (string.IsNullOrEmpty(_fromName)) {
                    return _name;
                } else {
                    return _fromName + "." + _name;
                }
            }
        }

        /// <summary>
        /// The name of the member or module that can be imported.
        /// </summary>
        public string ImportName {
            get {
                return _name;
            }
        }

        /// <summary>
        /// The name of the module it is imported from, if applicable.
        /// </summary>
        public string FromName {
            get {
                return _fromName;
            }
        }
    }
}
