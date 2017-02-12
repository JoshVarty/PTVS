using System;
using System.Runtime.Serialization;

namespace Microsoft.VisualStudioTools.Project {
    [Serializable]
    public sealed class PublishFailedException : Exception {
        public PublishFailedException(string message, Exception innerException)
            : base(message, innerException) {
        }

        private PublishFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
