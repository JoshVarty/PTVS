using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.PythonTools.InteractiveWindow;
using Microsoft.PythonTools.InteractiveWindow.Commands;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Repl {
    [Export(typeof(IInteractiveWindowCommand))]
    [ContentType(PythonCoreConstants.ContentType)]
    class SwitchModuleCommand : IInteractiveWindowCommand {
        public Task<ExecutionResult> Execute(IInteractiveWindow window, string arguments) {
            var remoteEval = window.Evaluator as IMultipleScopeEvaluator;
            Debug.Assert(remoteEval != null, "Evaluator does not support switching scope");
            if (remoteEval != null) {
                remoteEval.SetScope(arguments);
            }
            return ExecutionResult.Succeeded;
        }

        public string Description {
            get { return Strings.ReplSwitchModuleCommandDescription; }
        }

        public string Command {
            get { return "mod"; }
        }

        public IEnumerable<ClassificationSpan> ClassifyArguments(ITextSnapshot snapshot, Span argumentsSpan, Span spanToClassify) {
            yield break;
        }

        public string CommandLine {
            get {
                return "";
            }
        }

        public IEnumerable<string> DetailedDescription {
            get {
                yield return Description;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> ParametersDescription {
            get {
                yield break;
            }
        }

        public IEnumerable<string> Names {
            get {
                yield return "mod";
            }
        }
    }
}
