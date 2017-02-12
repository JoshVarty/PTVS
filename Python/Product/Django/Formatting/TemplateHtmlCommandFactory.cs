using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Web.Editor.Controller;

namespace Microsoft.PythonTools.Django.Formatting {
    [Export(typeof(ICommandFactory))]
    [ContentType(TemplateHtmlContentType.ContentTypeName)]
    internal class TemplateHtmlCommandFactory : ICommandFactory {
        public TemplateHtmlCommandFactory() {
        }

        public IEnumerable<ICommand> GetCommands(ITextView textView, ITextBuffer textBuffer) {
            return new ICommand[] {
                new TemplateFormatDocumentCommandHandler(textView),
                new TemplateFormatSelectionCommandHandler(textView),
            };
        }
    }
}

