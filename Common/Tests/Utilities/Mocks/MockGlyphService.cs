using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Language.Intellisense;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TestUtilities.Mocks {
    public class MockGlyphService : IGlyphService {
        #region IGlyphService Members

        public ImageSource GetGlyph(StandardGlyphGroup group, StandardGlyphItem item) {
            return new DrawingImage();
        }

        #endregion
    }
}
