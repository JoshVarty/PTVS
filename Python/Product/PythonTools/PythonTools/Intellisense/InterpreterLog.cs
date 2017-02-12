using System;
using System.ComponentModel.Composition;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.PythonTools.Intellisense {
    [Export(typeof(IInterpreterLog))]
    class InterpreterLog : IInterpreterLog {
        private readonly IVsActivityLog _activityLog;

        [ImportingConstructor]
        public InterpreterLog([Import(typeof(SVsServiceProvider))]IServiceProvider provider) {
            _activityLog = (IVsActivityLog)provider.GetService(typeof(SVsActivityLog));
        }

        public void Log(string msg) {
            _activityLog.LogEntry(
                (uint)__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR,
                "Python Tools", // TODO: Localization - use ProductTitle for this?
                msg
            );
        }
    }
}
