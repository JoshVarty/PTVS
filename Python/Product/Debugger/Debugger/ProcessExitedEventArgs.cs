using System;

namespace Microsoft.PythonTools.Debugger {
    class ProcessExitedEventArgs : EventArgs {
        private readonly int _exitCode;

        public ProcessExitedEventArgs(int exitCode) {
            _exitCode = exitCode;
        }

        public int ExitCode {
            get {
                return _exitCode;
            }
        }
    }
}
