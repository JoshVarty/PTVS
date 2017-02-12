using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Web.Editor.Services;

namespace Microsoft.PythonTools.Django.Intellisense {
    [Export(typeof(IIntellisenseControllerProvider)), ContentType(TemplateTagContentType.ContentTypeName), Order]
    internal class TemplateCompletionControllerProvider : IIntellisenseControllerProvider {
        private readonly ICompletionBroker _completionBroker;
        private readonly IQuickInfoBroker _quickInfoBroker;
        private readonly ISignatureHelpBroker _signatureHelpBroker;
        private readonly PythonToolsService _pyService;

        [ImportingConstructor]
        public TemplateCompletionControllerProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider, ICompletionBroker completionBroker, IQuickInfoBroker quickInfoBroker, ISignatureHelpBroker signatureHelpBroker) {
            _completionBroker = completionBroker;
            _quickInfoBroker = quickInfoBroker;
            _signatureHelpBroker = signatureHelpBroker;
            _pyService = (PythonToolsService)serviceProvider.GetService(typeof(PythonToolsService));
        }

        public IIntellisenseController TryCreateIntellisenseController(ITextView view, IList<ITextBuffer> subjectBuffers) {
            var completionController = ServiceManager.GetService<TemplateCompletionController>(view);
            if (completionController == null) {
                completionController = new TemplateCompletionController(_pyService, view, subjectBuffers, _completionBroker, _quickInfoBroker, _signatureHelpBroker);
                ServiceManager.AddService<TemplateCompletionController>(completionController, view);
            }
            return completionController;
        }
    }
}
