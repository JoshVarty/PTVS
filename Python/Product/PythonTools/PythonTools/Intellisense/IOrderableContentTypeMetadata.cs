using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Metadata which includes Ordering and Content Types
    /// 
    /// New in 1.1.
    /// </summary>
    public interface IOrderableContentTypeMetadata : IContentTypeMetadata, IOrderable {
    }
}
