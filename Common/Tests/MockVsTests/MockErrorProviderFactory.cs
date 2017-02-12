using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(IErrorProviderFactory))]
    public class MockErrorProviderFactory : IErrorProviderFactory {
        public SimpleTagger<ErrorTag> GetErrorTagger(ITextBuffer textBuffer) {
            return textBuffer.Properties.GetOrCreateSingletonProperty(
                () => new SimpleTagger<ErrorTag>(textBuffer)
            );
        }
    }
}
