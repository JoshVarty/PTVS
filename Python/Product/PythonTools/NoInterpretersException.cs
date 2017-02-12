using System;
using System.Runtime.Serialization;
using Microsoft.PythonTools.Project;

namespace Microsoft.PythonTools {
    [Serializable]
    public class NoInterpretersException : Exception {
        private readonly string _helpPage;

        public NoInterpretersException() : this(Strings.NoInterpretersAvailable) { }
        public NoInterpretersException(string message) : base(message) { }
        public NoInterpretersException(string message, Exception inner) : base(message, inner) { }

        public NoInterpretersException(string message, string helpPage)
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

        protected NoInterpretersException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            try {
                _helpPage = info.GetString("HelpPage");
            } catch (SerializationException) {
            }
        }
    }
}
