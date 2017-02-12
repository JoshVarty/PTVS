using System;

namespace Microsoft.PythonTools.Debugger {
    sealed class OutputEventArgs : EventArgs {
        private readonly string _output;
        private readonly PythonThread _thread;

        public OutputEventArgs(PythonThread thread, string output) {
            _thread = thread;
            _output = output;
        }

        public PythonThread Thread {
            get {
                return _thread;
            }
        }

        public string Output {
            get {
                return _output;
            }
        }
    }
}
