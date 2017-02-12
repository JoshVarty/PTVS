using System;

namespace Microsoft.PythonTools.Debugger {
    class BreakpointHitEventArgs : EventArgs {
        private readonly PythonBreakpoint _breakpoint;
        private readonly PythonThread _thread;

        public BreakpointHitEventArgs(PythonBreakpoint breakpoint, PythonThread thread) {
            _breakpoint = breakpoint;
            _thread = thread;
        }

        public PythonBreakpoint Breakpoint {
            get {
                return _breakpoint;
            }
        }

        public PythonThread Thread {
            get {
                return _thread;
            }
        }
    }
}
