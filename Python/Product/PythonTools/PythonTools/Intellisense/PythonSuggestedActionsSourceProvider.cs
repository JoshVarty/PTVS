using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Intellisense {
    [Export(typeof(ISuggestedActionsSourceProvider))]
    [Name("Python Suggested Actions")]
    [ContentType(PythonCoreConstants.ContentType)]
    [TextViewRole(PredefinedTextViewRoles.Analyzable)]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    class PythonSuggestedActionsSourceProvider : ISuggestedActionsSourceProvider {
        [Import(typeof(SVsServiceProvider))]
        internal IServiceProvider _provider = null;

        public ISuggestedActionsSource CreateSuggestedActionsSource(
            ITextView textView,
            ITextBuffer textBuffer
        ) {
            if (textView == null && textBuffer == null) {
                return null;
            }
            return new PythonSuggestedActionsSource(_provider, textView, textBuffer);
        }
    }
}
