using System;
using Microsoft.CookiecutterTools.Infrastructure;
using Microsoft.VisualStudio;

namespace Microsoft.CookiecutterTools.Commands {
    /// <summary>
    /// Provides the command for opening the cookiecutter window.
    /// </summary>
    class DeleteInstalledTemplateCommand : Command {
        private readonly CookiecutterToolWindow _window;

        public DeleteInstalledTemplateCommand(CookiecutterToolWindow window) {
            _window = window;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _window.DeleteSelection();
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return (sender, args) => {
                    var oleMenuCmd = (Microsoft.VisualStudio.Shell.OleMenuCommand)sender;
                    oleMenuCmd.Enabled = (_window.CanDeleteSelection());
                };
            }
        }

        public override int CommandId {
            get { return (int)VSConstants.VSStd97CmdID.Delete; }
        }
    }
}
