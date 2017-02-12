using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

namespace TestUtilities.Mocks {
    public class MockErrorProviderFactory : IErrorProviderFactory {
        public SimpleTagger<ErrorTag> GetErrorTagger(Microsoft.VisualStudio.Text.ITextBuffer textBuffer) {
            return textBuffer.Properties.GetOrCreateSingletonProperty<SimpleTagger<ErrorTag>>(
                () => new SimpleTagger<ErrorTag>(textBuffer)
            );
        }
    }
}
