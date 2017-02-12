using System;
using System.Runtime.Serialization;
using Microsoft.PythonTools.Project;

namespace Microsoft.PythonTools {
    [Serializable]
    public class MissingInterpreterException : Exception {
        private readonly string _helpPage;

        public MissingInterpreterException(string message) : base(message) { }
        public MissingInterpreterException(string message, Exception inner) : base(message, inner) { }

        public MissingInterpreterException(string message, string helpPage)
            : base(message) {
            _helpPage = helpPage;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            if (!string.IsNullOrEmpty(_helpPage)) {
                try {
                    info.AddValue("HelpPage", _helpPage);
                } catch (SerializationException) {
                }
            }
        }

        public string HelpPage { get { return _helpPage; } }

        protected MissingInterpreterException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            try {
                _helpPage = info.GetString("HelpPage");
            } catch (SerializationException) {
            }
        }
    }
}
