using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.PythonTools {
    internal static class PythonCoreConstants {
        public const string ContentType = "Python";
        public const string BaseRegistryKey = "PythonTools";
        
        [Export, Name(ContentType), BaseDefinition("code")]
        internal static ContentTypeDefinition ContentTypeDefinition = null;
    }
}
