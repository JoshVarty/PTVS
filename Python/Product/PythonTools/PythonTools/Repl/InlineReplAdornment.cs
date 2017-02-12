using System.ComponentModel.Composition;
using Microsoft.PythonTools.InteractiveWindow;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Repl {
    [Export(typeof(IViewTaggerProvider))]
    [TagType(typeof(IntraTextAdornmentTag))]
    [ContentType(PredefinedInteractiveContentTypes.InteractiveContentTypeName)]
    internal class InlineReplAdornmentProvider : IViewTaggerProvider {
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag {
            if (buffer == null || textView == null || typeof(T) != typeof(IntraTextAdornmentTag)) {
                return null;
            }

            return (ITagger<T>)textView.Properties.GetOrCreateSingletonProperty(
                typeof(InlineReplAdornmentManager),
                () => new InlineReplAdornmentManager(textView)
            );
        }

        internal static InlineReplAdornmentManager GetManager(ITextView view) {
            InlineReplAdornmentManager result;
            if (!view.Properties.TryGetProperty(typeof(InlineReplAdornmentManager), out result)) {
                return null;
            }
            return result;
        }
    }
}
