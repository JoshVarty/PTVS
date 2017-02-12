using System;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Language.Intellisense;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(IGlyphService))]
    class MockGlyphService : IGlyphService {
        public ImageSource GetGlyph(StandardGlyphGroup group, StandardGlyphItem item) {
            return null;
        }
    }
}
