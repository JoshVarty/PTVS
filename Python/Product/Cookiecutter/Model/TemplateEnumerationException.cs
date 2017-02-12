using System;
using System.Threading.Tasks;

namespace Microsoft.CookiecutterTools.Model {
    [Serializable]
    class TemplateEnumerationException : Exception {
        public TemplateEnumerationException() {
        }

        public TemplateEnumerationException(string message, Exception innerException)
            : base(message, innerException) {
        }
    }
}
