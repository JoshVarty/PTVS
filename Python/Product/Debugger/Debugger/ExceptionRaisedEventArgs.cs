using System;

namespace Microsoft.PythonTools.Debugger {
    class ExceptionRaisedEventArgs : EventArgs {
        private readonly PythonException _exception;
        private readonly PythonThread _thread;

        public ExceptionRaisedEventArgs(PythonThread thread, PythonException exception) {
            _thread = thread;
            _exception = exception;
        }

        public PythonException Exception {
            get {
                return _exception;
            }
        }

        public PythonThread Thread {
            get {
                return _thread;
            }
        }
    }
}
