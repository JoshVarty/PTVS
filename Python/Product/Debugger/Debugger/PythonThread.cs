using System.Collections.Generic;

namespace Microsoft.PythonTools.Debugger {
    class PythonThread {
        private readonly long _identity;
        private readonly PythonProcess _process;
        private readonly bool _isWorkerThread;
        private string _name;
        private IList<PythonStackFrame> _frames;

        internal PythonThread(PythonProcess process, long identity, bool isWorkerThread) {
            _process = process;
            _identity = identity;
            _isWorkerThread = isWorkerThread;
            _name = "";
        }

        public void StepInto() {
            _process.SendStepInto(_identity);
        }

        public void StepOver() {
            _process.SendStepOver(_identity);
        }

        public void StepOut() {
            _process.SendStepOut(_identity);
        }

        public void Resume() {
            _process.SendResumeThread(_identity);
        }

        public void AutoResume() {
            _process.AutoResumeThread(_identity);
        }

        public bool IsWorkerThread {
            get {
                return _isWorkerThread;
            }
        }

        internal void ClearSteppingState() {
            _process.SendClearStepping(_identity);
        }

        public IList<PythonStackFrame> Frames {
            get {
                return _frames;
            }
            set {
                _frames = value;
            }
        }

        public PythonProcess Process {
            get {
                return _process;
            }
        }

        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        internal long Id {
            get {
                return _identity;
            }
        }
    }
}
