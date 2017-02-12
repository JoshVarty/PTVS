using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(ITextMarkerProviderFactory))]
    class MockTextMarkerProviderFactory : ITextMarkerProviderFactory {
        public SimpleTagger<TextMarkerTag> GetTextMarkerTagger(ITextBuffer textBuffer) {
            SimpleTagger<TextMarkerTag> tagger;
            if (textBuffer.Properties.TryGetProperty(typeof(SimpleTagger<TextMarkerTag>), out tagger)) {
                return tagger;
            }
            tagger = new SimpleTagger<TextMarkerTag>(textBuffer);
            textBuffer.Properties.AddProperty(typeof(SimpleTagger<TextMarkerTag>), tagger);
            return tagger;
        }
    }
}
