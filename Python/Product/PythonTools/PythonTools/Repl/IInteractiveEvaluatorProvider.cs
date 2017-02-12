using System;
using System.Collections.Generic;
using Microsoft.PythonTools.InteractiveWindow;

namespace Microsoft.PythonTools.Repl {
    interface IInteractiveEvaluatorProvider {
        IInteractiveEvaluator GetEvaluator(string evaluatorId);

        /// <summary>
        /// Returns a list of display name - evaluatorId pairs.
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> GetEvaluators();

        /// <summary>
        /// The result of <see cref="GetEvaluators"/> has changed.
        /// </summary>
        event EventHandler EvaluatorsChanged;
    }
}
