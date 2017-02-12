using System.Collections.Generic;

namespace Microsoft.VisualStudioTools.MockVsTests {
    public interface IContentTypeDefinitionMetadata {
        string Name { get; }

        [System.ComponentModel.DefaultValue(null)]
        IEnumerable<string> BaseDefinition { get; }
    }
}
