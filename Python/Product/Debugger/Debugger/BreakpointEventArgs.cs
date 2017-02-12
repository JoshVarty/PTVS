using System;

namespace Microsoft.PythonTools.Debugger {
    class BreakpointEventArgs : EventArgs {
        private readonly PythonBreakpoint _breakpoint;

        public BreakpointEventArgs(PythonBreakpoint breakpoint) {
            _breakpoint = breakpoint;
        }

        public PythonBreakpoint Breakpoint {
            get {
                return _breakpoint;
            }
        }
    }
}
