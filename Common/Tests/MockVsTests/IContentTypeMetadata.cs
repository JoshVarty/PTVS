using System.Collections.Generic;

namespace Microsoft.VisualStudioTools.MockVsTests {
    public interface IContentTypeMetadata {
        IEnumerable<string> ContentTypes { get; }
    }
}
