using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.Web.Editor.Completion;
using Microsoft.Web.Editor.Services;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal class TemplateTypingCommandHandler : TypingCommandHandler {
        private readonly IEditorOperations _editorOperations;
        private readonly IEditorOptions _editorOptions;

        public TemplateTypingCommandHandler(
            ITextView textView,
            ITextBuffer textBuffer,
            IEditorOptions editorOptions,
            IEditorOperations editorOperations)
            : base(textView, _ => textBuffer)
        {
            _editorOperations = editorOperations;
            _editorOptions = editorOptions;
        }

        protected override CompletionController CompletionController {
            get {
                return ServiceManager.GetService<TemplateCompletionController>(TextView);
            }
        }
    }

}
