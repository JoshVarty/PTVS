using System;
using Microsoft.CookiecutterTools.Infrastructure;

namespace Microsoft.CookiecutterTools.Commands {
    /// <summary>
    /// Provides the command for opening the cookiecutter window.
    /// </summary>
    class HomeCommand : Command {
        private readonly CookiecutterToolWindow _window;

        public HomeCommand(CookiecutterToolWindow window) {
            _window = window;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _window.Home();
        }

        public override int CommandId {
            get { return (int)PackageIds.cmdidHome; }
        }
    }
}
