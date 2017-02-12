using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.BraceCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Editor.BraceCompletion {
    [Export(typeof(IBraceCompletionContextProvider))]
    [BracePair('(', ')')]
    [BracePair('[', ']')]
    [BracePair('{', '}')]
    [BracePair('"', '"')]
    [BracePair('\'', '\'')]
    [ContentType(PythonCoreConstants.ContentType)]
    internal sealed class BraceCompletionContextProvider : IBraceCompletionContextProvider {
        public bool TryCreateContext(ITextView textView, SnapshotPoint openingPoint, char openingBrace, char closingBrace, out IBraceCompletionContext context) {
            if (IsValidBraceCompletionContext(openingPoint)) {
                context = new BraceCompletionContext();
                return true;
            } else {
                context = null;
                return false;
            }
        }

        private bool IsValidBraceCompletionContext(SnapshotPoint openingPoint) {
            Debug.Assert(openingPoint.Position >= 0, "SnapshotPoint.Position should always be zero or positive.");

            if (openingPoint.Position > 0) {
                var classificationSpans = openingPoint.Snapshot.TextBuffer
                    .GetPythonClassifier()
                    .GetClassificationSpans(new SnapshotSpan(openingPoint - 1, 1));

                foreach (var span in classificationSpans) {
                    if (span.ClassificationType.IsOfType("comment")) {
                        return false;
                    }
                    if (span.ClassificationType.IsOfType("string")) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
