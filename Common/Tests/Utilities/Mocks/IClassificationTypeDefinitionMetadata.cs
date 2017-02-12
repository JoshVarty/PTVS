using System.Collections.Generic;

namespace TestUtilities.Mocks {
    public interface IClassificationTypeDefinitionMetadata {
        string Name { get; }
        [System.ComponentModel.DefaultValue(null)]
        IEnumerable<string> BaseDefinition { get; }
    }
}
