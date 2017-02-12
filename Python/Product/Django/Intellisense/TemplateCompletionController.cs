using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.Web.Editor.Completion;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal class TemplateCompletionController : CompletionController {
        private readonly PythonToolsService _pyService;

        public TemplateCompletionController(
            PythonToolsService pyService,
            ITextView textView,
            IList<ITextBuffer> subjectBuffers,
            ICompletionBroker completionBroker,
            IQuickInfoBroker quickInfoBroker,
            ISignatureHelpBroker signatureBroker) :
            base(textView, subjectBuffers, completionBroker, quickInfoBroker, signatureBroker) {
            _pyService = pyService;
        }

        public override bool IsTriggerChar(char typedCharacter) {
            const string triggerChars = " |.";
            return _pyService.AdvancedOptions.AutoListMembers && !HasActiveCompletionSession && triggerChars.IndexOf(typedCharacter) >= 0;
        }

        public override bool IsCommitChar(char typedCharacter) {
            if (!HasActiveCompletionSession) {
                return false;
            }

            if (typedCharacter == '\n' || typedCharacter == '\t') {
                return true;
            }

            return _pyService.AdvancedOptions.CompletionCommittedBy.IndexOf(typedCharacter) > 0;
        }

        protected override bool IsRetriggerChar(ICompletionSession session, char typedCharacter) {
            if (typedCharacter == ' ') {
                return true;
            }

            return base.IsRetriggerChar(session, typedCharacter);
        }
    }
}
