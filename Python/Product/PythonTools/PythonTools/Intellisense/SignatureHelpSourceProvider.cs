using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Intellisense {
    [Export(typeof(ISignatureHelpSourceProvider)), ContentType(PythonCoreConstants.ContentType), Order, Name("Python Signature Help Source")]
    class SignatureHelpSourceProvider : ISignatureHelpSourceProvider {
        internal readonly IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public SignatureHelpSourceProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public ISignatureHelpSource TryCreateSignatureHelpSource(ITextBuffer textBuffer) {
            return new SignatureHelpSource(this, textBuffer);
        }
    }
}
