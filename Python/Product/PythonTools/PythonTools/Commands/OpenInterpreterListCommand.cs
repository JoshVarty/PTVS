using System;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Commands {
    /// <summary>
    /// Provides the command for opening the interpreter list.
    /// </summary>
    class OpenInterpreterListCommand : Command {
        private readonly IServiceProvider _provider;

        public OpenInterpreterListCommand(IServiceProvider provider) {
            _provider = provider;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _provider.ShowInterpreterList();
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidInterpreterList; }
        }
    }
}
