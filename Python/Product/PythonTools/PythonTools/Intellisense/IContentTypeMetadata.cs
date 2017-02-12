using System.Collections.Generic;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// New in 1.1
    /// </summary>
    public interface IContentTypeMetadata {
        IEnumerable<string> ContentTypes { get; }
    }
}
