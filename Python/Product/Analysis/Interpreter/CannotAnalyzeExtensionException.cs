using System;
using System.Runtime.Serialization;

namespace Microsoft.PythonTools.Interpreter {
    [Serializable]
    public class CannotAnalyzeExtensionException : Exception {
        public CannotAnalyzeExtensionException() : base() { }
        public CannotAnalyzeExtensionException(string msg) : base(msg) { }
        public CannotAnalyzeExtensionException(string message, Exception innerException)
            : base(message, innerException) {
        }

        protected CannotAnalyzeExtensionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
