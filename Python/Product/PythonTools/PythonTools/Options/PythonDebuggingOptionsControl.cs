using System;
using System.Windows.Forms;

namespace Microsoft.PythonTools.Options {
    public partial class PythonDebuggingOptionsControl : UserControl {
        public PythonDebuggingOptionsControl() {
            InitializeComponent();
        }

        internal void SyncControlWithPageSettings(PythonToolsService pyService) {
            _promptOnBuildError.Checked = pyService.DebuggerOptions.PromptBeforeRunningWithBuildError;
            _waitOnAbnormalExit.Checked = pyService.DebuggerOptions.WaitOnAbnormalExit;
            _waitOnNormalExit.Checked = pyService.DebuggerOptions.WaitOnNormalExit;
            _teeStdOut.Checked = pyService.DebuggerOptions.TeeStandardOutput;
            _breakOnSystemExitZero.Checked = pyService.DebuggerOptions.BreakOnSystemExitZero;
            _debugStdLib.Checked = pyService.DebuggerOptions.DebugStdLib;
        }

        internal void SyncPageWithControlSettings(PythonToolsService pyService) {
            pyService.DebuggerOptions.PromptBeforeRunningWithBuildError = _promptOnBuildError.Checked;
            pyService.DebuggerOptions.WaitOnAbnormalExit = _waitOnAbnormalExit.Checked;
            pyService.DebuggerOptions.WaitOnNormalExit = _waitOnNormalExit.Checked;
            pyService.DebuggerOptions.TeeStandardOutput = _teeStdOut.Checked;
            pyService.DebuggerOptions.BreakOnSystemExitZero = _breakOnSystemExitZero.Checked;
            pyService.DebuggerOptions.DebugStdLib = _debugStdLib.Checked;
        }
    }
}
