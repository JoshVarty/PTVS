using System.Collections.Generic;

namespace Microsoft.PythonTools.Django.TemplateParsing {
    public interface IContentTypeMetadata {
        IEnumerable<string> ContentTypes { get; }
    }
}
