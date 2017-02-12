using System;

namespace Microsoft.PythonTools.Debugger {
    /// <summary>
    /// Event args for start/stop of threads.
    /// </summary>
    class ThreadEventArgs : EventArgs {
        private readonly PythonThread _thread;

        public ThreadEventArgs(PythonThread thread) {
            _thread = thread;
        }

        public PythonThread Thread {
            get {
                return _thread;
            }
        }
    }
}
