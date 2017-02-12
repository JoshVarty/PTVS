using Microsoft.VisualStudio.Text.Editor;
using Microsoft.Web.Editor.Completion;
using Microsoft.Web.Editor.Services;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal class TemplateCompletionCommandHandler : CompletionCommandHandler {
        public TemplateCompletionCommandHandler(ITextView textView)
            : base(textView) {
        }

        public override CompletionController CompletionController {
            get {
                return ServiceManager.GetService<TemplateCompletionController>(TextView);
            }
        }
    }
}
