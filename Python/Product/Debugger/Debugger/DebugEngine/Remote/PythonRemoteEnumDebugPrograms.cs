using System;
using Microsoft.VisualStudio.Debugger.Interop;

namespace Microsoft.PythonTools.Debugger.Remote {
    internal class PythonRemoteEnumDebugPrograms : PythonRemoteEnumDebug<IDebugProgram2>, IEnumDebugPrograms2 {

        public readonly PythonRemoteDebugProcess _process;

        public PythonRemoteEnumDebugPrograms(PythonRemoteDebugProcess process)
            : base(new PythonRemoteDebugProgram(process)) {
            this._process = process;
        }

        public int Clone(out IEnumDebugPrograms2 ppEnum) {
            ppEnum = new PythonRemoteEnumDebugPrograms(_process);
            return 0;
        }
    }
}
