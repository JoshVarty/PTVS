using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Intellisense {
    [Export(typeof(IQuickInfoSourceProvider)), ContentType(PythonCoreConstants.ContentType), Order, Name("Python Quick Info Source")]
    class QuickInfoSourceProvider : IQuickInfoSourceProvider {
        private readonly IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public QuickInfoSourceProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer) {
            return new QuickInfoSource(textBuffer);
        }
    }
}
