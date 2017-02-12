using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.BraceCompletion;

namespace Microsoft.PythonTools.Editor.BraceCompletion {
    [Export(typeof(IBraceCompletionContext))]
    internal class BraceCompletionContext : IBraceCompletionContext {
        public bool AllowOverType(IBraceCompletionSession session) {
            return true;
        }

        public void Finish(IBraceCompletionSession session) {
        }

        public void OnReturn(IBraceCompletionSession session) {
        }

        public void Start(IBraceCompletionSession session) {
        }
    }
}
