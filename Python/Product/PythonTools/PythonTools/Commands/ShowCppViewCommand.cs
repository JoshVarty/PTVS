using System;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools.Commands {
    using DebuggerOptions = Microsoft.PythonTools.DkmDebugger.DebuggerOptions;

    internal class ShowCppViewCommand : DkmDebuggerCommand {
        public ShowCppViewCommand(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidShowCppView; }
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    base.BeforeQueryStatus(sender, args);
                    var cmd = (OleMenuCommand)sender;
                    cmd.Checked = DebuggerOptions.ShowCppViewNodes;
                };
            }
        }

        public override void DoCommand(object sender, EventArgs args) {
            DebuggerOptions.ShowCppViewNodes = !DebuggerOptions.ShowCppViewNodes;

            // A hackish way to force debugger to refresh its views, so that our EE is requeried and can use the new option value.
            var debugger = _serviceProvider.GetDTE().Debugger;
            debugger.HexDisplayMode = debugger.HexDisplayMode;
        }
    }
}
