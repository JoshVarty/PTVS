using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.PythonTools.InteractiveWindow;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools.Repl {
    [Export(typeof(IInteractiveEvaluatorProvider))]
    class PythonDebugReplEvaluatorProvider : IInteractiveEvaluatorProvider {
        private const string _debugReplGuid = "BA417560-5A78-46F1-B065-638D27E1CDD0";
        private readonly IServiceProvider _serviceProvider;

        public event EventHandler EvaluatorsChanged { add { } remove { } }

        [ImportingConstructor]
        public PythonDebugReplEvaluatorProvider([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IInteractiveEvaluator GetEvaluator(string replId) {
            if (replId.StartsWith(_debugReplGuid)) {
                return new PythonDebugReplEvaluator(_serviceProvider);
            }
            return null;
        }

        public IEnumerable<KeyValuePair<string, string>> GetEvaluators() {
            yield return new KeyValuePair<string, string>(Strings.DebugReplDisplayName, GetDebugReplId());
        }

        internal static string GetDebugReplId() {
            return _debugReplGuid;
        }
    }
}
