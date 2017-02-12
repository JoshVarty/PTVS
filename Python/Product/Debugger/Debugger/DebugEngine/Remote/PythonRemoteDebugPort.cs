using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

namespace Microsoft.PythonTools.Debugger.Remote {
    internal class PythonRemoteDebugPort : IDebugPort2 {
        private readonly PythonRemoteDebugPortSupplier _supplier;
        private readonly IDebugPortRequest2 _request;
        private readonly Guid _guid = Guid.NewGuid();
        private readonly Uri _uri;

        public PythonRemoteDebugPort(PythonRemoteDebugPortSupplier supplier, IDebugPortRequest2 request, Uri uri) {
            _supplier = supplier;
            _request = request;
            _uri = uri;
        }

        public Uri Uri {
            get { return _uri; }
        }

        public int EnumProcesses(out IEnumDebugProcesses2 ppEnum) {
            var process = PythonRemoteDebugProcess.Connect(this);
            if (process == null) {
                ppEnum = null;
                return VSConstants.E_FAIL;
            } else {
                ppEnum = new PythonRemoteEnumDebugProcesses(process);
                return VSConstants.S_OK;
            }
        }

        public int GetPortId(out Guid pguidPort) {
            pguidPort = _guid;
            return 0;
        }

        public int GetPortName(out string pbstrName) {
            pbstrName = _uri.ToString();
            return VSConstants.S_OK;
        }

        public int GetPortRequest(out IDebugPortRequest2 ppRequest) {
            ppRequest = _request;
            return VSConstants.S_OK;
        }

        public int GetPortSupplier(out IDebugPortSupplier2 ppSupplier) {
            ppSupplier = _supplier;
            return VSConstants.S_OK;
        }

        public int GetProcess(AD_PROCESS_ID ProcessId, out IDebugProcess2 ppProcess) {
            throw new NotImplementedException();
        }
    }
}
