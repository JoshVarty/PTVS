using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Intellisense {
    [Export(typeof(ICompletionSourceProvider)), ContentType(PythonCoreConstants.ContentType), Order, Name("CompletionProvider")]
    internal class CompletionSourceProvider : ICompletionSourceProvider {
        internal readonly IGlyphService _glyphService;
        internal readonly PythonToolsService _pyService;
        internal readonly IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public CompletionSourceProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider, IGlyphService glyphService) {
            _pyService = serviceProvider.GetPythonToolsService();
            _glyphService = glyphService;
            _serviceProvider = serviceProvider;
        }

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer) {
            return new CompletionSource(this, textBuffer);
        }
    }
}
