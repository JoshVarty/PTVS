using Microsoft.VisualStudio.Shell;

namespace Microsoft.VisualStudioTools.MockVsTests {
    /// <summary>
    /// Stores information about registered language services.
    /// </summary>
    class LanguageServiceInfo {
        public readonly ProvideLanguageServiceAttribute Attribute;

        public LanguageServiceInfo(ProvideLanguageServiceAttribute attr) {
            Attribute = attr;
        }
    }

}
