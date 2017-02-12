using System;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools.Commands {
    using DebuggerOptions = Microsoft.PythonTools.DkmDebugger.DebuggerOptions;

    internal class ShowNativePythonFrames : DkmDebuggerCommand {
        public ShowNativePythonFrames(IServiceProvider serviceProvider)
            : base(serviceProvider) {
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidShowNativePythonFrames; }
        }

        protected override bool IsPythonDeveloperCommand {
            get { return true; }
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    base.BeforeQueryStatus(sender, args);
                    var cmd = (OleMenuCommand)sender;
                    cmd.Checked = DebuggerOptions.ShowNativePythonFrames;
                };
            }
        }

        public override void DoCommand(object sender, EventArgs args) {
            DebuggerOptions.ShowNativePythonFrames = !DebuggerOptions.ShowNativePythonFrames;

            // A hackish way to force debugger to refresh its views, so that our CallStackFilter is requeried and can use the new option value.
            var debugger = _serviceProvider.GetDTE().Debugger;
            debugger.HexDisplayMode = debugger.HexDisplayMode;
        }
    }
}
