using System;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Repl;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Commands {
    class SendToDefiningModuleCommand : SendToReplCommand {
        public SendToDefiningModuleCommand(IServiceProvider serviceProvider)
            : base(serviceProvider) {
        }

        public override void DoCommand(object sender, EventArgs args) {
            var activeView = PythonToolsPackage.GetActiveTextView(_serviceProvider);
            var pyProj = activeView.TextBuffer.GetProject(_serviceProvider);
            var analyzer = activeView.GetAnalyzerAtCaret(_serviceProvider);
            var window = ExecuteInReplCommand.EnsureReplWindow(_serviceProvider, analyzer, pyProj);
            var eval = window.InteractiveWindow.Evaluator as PythonReplEvaluator;

            string path = activeView.GetFilePath();
            string scope;
            if (path != null && (scope = eval.GetScopeByFilename(path)) != null) {
                // we're now in the correct module, execute the code
                window.InteractiveWindow.Operations.Cancel();
                // TODO: get correct prompt
                window.InteractiveWindow.WriteLine(">>>" + " $module " + scope);
                eval.SetScope(scope);

                base.DoCommand(sender, args);
            } else {
                window.InteractiveWindow.WriteLine("Could not find defining module.");
            }
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidSendToDefiningModule; }
        }
    }
}
