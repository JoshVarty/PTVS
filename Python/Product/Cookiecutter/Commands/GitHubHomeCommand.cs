using System;
using Microsoft.CookiecutterTools.Infrastructure;

namespace Microsoft.CookiecutterTools.Commands {
    /// <summary>
    /// Provides the command for opening the cookiecutter window.
    /// </summary>
    class GitHubCommand : Command {
        private readonly CookiecutterToolWindow _window;
        private readonly int _commandId;

        public GitHubCommand(CookiecutterToolWindow window, int commandId) {
            _window = window;
            _commandId = commandId;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _window.NavigateToGitHub(_commandId);
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    var oleMenuCmd = (Microsoft.VisualStudio.Shell.OleMenuCommand)sender;
                    oleMenuCmd.Enabled = (_window.CanNavigateToGitHub());
                };
            }
        }

        public override int CommandId {
            get { return _commandId; }
        }
    }
}
