using System;

namespace Microsoft.PythonTools.Debugger {
    class ModuleLoadedEventArgs : EventArgs {
        private readonly PythonModule _module;

        public ModuleLoadedEventArgs(PythonModule module) {
            _module = module;
        }

        public PythonModule Module {
            get {
                return _module;
            }
        }
    }
}
