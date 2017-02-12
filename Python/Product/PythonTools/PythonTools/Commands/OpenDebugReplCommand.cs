using System;
using Microsoft.PythonTools.Repl;
using Microsoft.PythonTools.InteractiveWindow.Shell;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Commands {
    /// <summary>
    /// Provides the command for starting the Python Debug REPL window.
    /// </summary>
    class OpenDebugReplCommand : Command {
        private readonly IServiceProvider _serviceProvider;

        public OpenDebugReplCommand(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        internal static IVsInteractiveWindow/*!*/ EnsureReplWindow(IServiceProvider serviceProvider) {
            var compModel = serviceProvider.GetComponentModel();
            var provider = compModel.GetService<InteractiveWindowProvider>();

            return provider.OpenOrCreate(PythonDebugReplEvaluatorProvider.GetDebugReplId());
        }

        public override void DoCommand(object sender, EventArgs args) {
            EnsureReplWindow(_serviceProvider).Show(true);
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidDebugReplWindow; }
        }
    }
}
