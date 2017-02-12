using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.PythonTools.Ipc.Json {
    [Serializable]
    public sealed class FailedRequestException : Exception {
        private readonly Response _response;

        public FailedRequestException(string message, Response response) : base(message) {
            _response = response;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            info.AddValue("Response", _response);
        }

        public Response Response => _response;
    }
}
