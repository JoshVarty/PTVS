using System;
using Microsoft.CookiecutterTools.Infrastructure;

namespace Microsoft.CookiecutterTools.Commands {
    /// <summary>
    /// Provides the command for opening the cookiecutter window.
    /// </summary>
    class UpdateCommand : Command {
        private readonly CookiecutterToolWindow _window;

        public UpdateCommand(CookiecutterToolWindow window) {
            _window = window;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _window.UpdateSelection();
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    var oleMenuCmd = (Microsoft.VisualStudio.Shell.OleMenuCommand)sender;
                    oleMenuCmd.Enabled = (_window.CanUpdateSelection());
                };
            }
        }

        public override int CommandId {
            get { return (int)PackageIds.cmdidUpdateTemplate; }
        }
    }
}
