using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.Web.Editor;
using Microsoft.Web.Editor.Controller.Command;

namespace Microsoft.PythonTools.Django.Formatting {
    internal class TemplateFormatDocumentCommandHandler : EditingCommand {
        public TemplateFormatDocumentCommandHandler(ITextView textView)
            : base(textView, new CommandId(VSConstants.VSStd2K, (int)VSConstants.VSStd2KCmdID.FORMATDOCUMENT)) {
        }

        public override CommandStatus Status(Guid group, int id) {
            return CommandStatus.NotSupported;
        }

        public override CommandResult Invoke(Guid group, int id, object inputArg, ref object outputArg) {
            return CommandResult.NotSupported;
        }
    }
}
