using System;
using Microsoft.CookiecutterTools.Infrastructure;

namespace Microsoft.CookiecutterTools.Commands {
    /// <summary>
    /// Provides the command for opening the cookiecutter window.
    /// </summary>
    class CheckForUpdatesCommand : Command {
        private readonly CookiecutterToolWindow _window;

        public CheckForUpdatesCommand(CookiecutterToolWindow window) {
            _window = window;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _window.CheckForUpdates();
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    var oleMenuCmd = (Microsoft.VisualStudio.Shell.OleMenuCommand)sender;
                    oleMenuCmd.Enabled = (_window.CanCheckForUpdates());
                };
            }
        }

        public override int CommandId {
            get { return (int)PackageIds.cmdidCheckForUpdates; }
        }
    }
}
