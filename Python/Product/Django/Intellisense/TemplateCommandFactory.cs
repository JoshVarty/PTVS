using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Web.Editor.Controller;

namespace Microsoft.PythonTools.Django.Intellisense {
    [Export(typeof(ICommandFactory))]
    [ContentType(TemplateTagContentType.ContentTypeName)]
    internal class TemplateCommandFactory : ICommandFactory {
        [Import(typeof(IEditorOptionsFactoryService))]
        IEditorOptionsFactoryService _editorOptionsFactory = null;

        [Import(typeof(IEditorOperationsFactoryService))]
        IEditorOperationsFactoryService _editorOperationsFactory = null;

        public TemplateCommandFactory() {
        }

        public IEnumerable<ICommand> GetCommands(ITextView textView, ITextBuffer textBuffer) {
            return new ICommand[] {
                new TemplateTypingCommandHandler(
                    textView, textBuffer,
                    _editorOptionsFactory.GetOptions(textView),
                    _editorOperationsFactory.GetEditorOperations(textView)),
                new TemplateCompletionCommandHandler(textView)
            };
        }
    }
}
