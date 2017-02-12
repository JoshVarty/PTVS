using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonModuleContext : IModuleContext {
        private bool _showClr;
        public static readonly IronPythonModuleContext ShowClrInstance = new IronPythonModuleContext(true);
        public static readonly IronPythonModuleContext DontShowClrInstance = new IronPythonModuleContext(false);

        public IronPythonModuleContext() {
        }

        public IronPythonModuleContext(bool showClr) {
            _showClr = showClr;
        }

        public bool ShowClr {
            get { return _showClr; }
            set { _showClr = value; }
        }
    }
}
