using System;
using Microsoft.CookiecutterTools.Infrastructure;

namespace Microsoft.CookiecutterTools.Commands {
    /// <summary>
    /// Provides the command for opening the cookiecutter window.
    /// </summary>
    class RunCommand : Command {
        private readonly CookiecutterToolWindow _window;

        public RunCommand(CookiecutterToolWindow window) {
            _window = window;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _window.RunSelection();
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    var oleMenuCmd = (Microsoft.VisualStudio.Shell.OleMenuCommand)sender;
                    oleMenuCmd.Enabled = (_window.CanRunSelection());
                };
            }
        }

        public override int CommandId {
            get { return (int)PackageIds.cmdidRun; }
        }
    }
}
