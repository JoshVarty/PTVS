using Microsoft.VisualStudio.Debugger.Interop;

namespace Microsoft.PythonTools.Debugger.Remote {
    internal class PythonRemoteEnumDebugProcesses : PythonRemoteEnumDebug<IDebugProcess2>, IEnumDebugProcesses2 {
        private readonly PythonRemoteDebugProcess _process;

        public PythonRemoteEnumDebugProcesses(PythonRemoteDebugProcess process)
            : base(process) {
            this._process = process;
        }

        public int Clone(out IEnumDebugProcesses2 ppEnum) {
            ppEnum = new PythonRemoteEnumDebugProcesses(_process);
            return 0;
        }
    }
}
