using System;

namespace Microsoft.PythonTools.Debugger.DebugEngine {
    /// <summary>
    /// Event args for start/stop of engines.
    /// </summary>
    class AD7EngineEventArgs : EventArgs {
        private readonly AD7Engine _engine;

        public AD7EngineEventArgs(AD7Engine engine) {
            _engine = engine;
        }

        public AD7Engine Engine {
            get {
                return _engine;
            }
        }
    }
}
