using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Django.Intellisense {
    [Export(typeof(ICompletionSourceProvider)), ContentType(TemplateTagContentType.ContentTypeName), Order, Name("DjangoCompletionSourceProvider")]
    internal class DjangoCompletionSourceProvider : ICompletionSourceProvider {
        internal readonly IGlyphService _glyphService;
        internal readonly IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public DjangoCompletionSourceProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider, IGlyphService glyphService) {
            _serviceProvider = serviceProvider;
            _glyphService = glyphService;
        }

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer) {
            var filename = textBuffer.GetFileName();
            if (filename != null) {
                var project = DjangoPackage.GetProject(_serviceProvider, filename);
                if (project != null) {
                    return new DjangoCompletionSource(_glyphService, project.Analyzer, _serviceProvider, textBuffer);
                }
            }
            return null;
        }
    }
}
