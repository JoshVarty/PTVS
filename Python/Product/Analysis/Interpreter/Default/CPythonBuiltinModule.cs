
namespace Microsoft.PythonTools.Interpreter.Default {
    class CPythonBuiltinModule : CPythonModule, IBuiltinPythonModule {
        public CPythonBuiltinModule(SharedDatabaseState typeDb, string moduleName, string filename, bool isBuiltin)
            : base(typeDb, moduleName, filename, isBuiltin) {
        }

        public IMember GetAnyMember(string name) {
            if (string.IsNullOrEmpty(name)) {
                return null;
            }

            EnsureLoaded();

            IMember res;
            if (_members.TryGetValue(name, out res) || (_hiddenMembers != null && _hiddenMembers.TryGetValue(name, out res))) {
                return res;
            }
            return null;
        }
    }
}
