using System;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools.Commands {
    using DebuggerOptions = Microsoft.PythonTools.DkmDebugger.DebuggerOptions;

    internal class UsePythonStepping : DkmDebuggerCommand {
        public UsePythonStepping(IServiceProvider serviceProvider)
            : base(serviceProvider) {
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidUsePythonStepping; }
        }

        protected override bool IsPythonDeveloperCommand {
            get { return true; }
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    base.BeforeQueryStatus(sender, args);
                    var cmd = (OleMenuCommand)sender;
                    cmd.Checked = DebuggerOptions.UsePythonStepping;
                };
            }
        }

        public override void DoCommand(object sender, EventArgs args) {
            DebuggerOptions.UsePythonStepping = !DebuggerOptions.UsePythonStepping;
        }
    }
}
