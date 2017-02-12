using System.Collections.Generic;
using System.Text;
using Microsoft.CookiecutterTools.Telemetry;

namespace CookiecutterTests {
    internal sealed class TestTelemetryRecorder : ITelemetryRecorder, ITelemetryTestSupport {
        private StringBuilder _stringBuilder = new StringBuilder();

        #region ITelemetryRecorder
        public bool IsEnabled {
            get { return true; }
        }

        public bool CanCollectPrivateInformation {
            get { return true; }
        }

        public void RecordEvent(string eventName, object parameters = null) {
            _stringBuilder.AppendLine(eventName);
            if (parameters != null) {
                if (parameters is string) {
                    WriteProperty("Value", parameters as string);
                } else {
                    WriteDictionary(DictionaryExtension.FromAnonymousObject(parameters));
                }
            }
        }
        #endregion

        #region ITelemetryTestSupport
        public void Reset() {
            _stringBuilder.Clear();
        }

        public string SessionLog {
            get { return _stringBuilder.ToString(); }
        }
        #endregion

        public void Dispose() { }

        private void WriteDictionary(IDictionary<string, object> dict) {
            foreach (KeyValuePair<string, object> kvp in dict) {
                WriteProperty(kvp.Key, kvp.Value);
            }
        }

        private void WriteProperty(string name, object value) {
            _stringBuilder.Append('\t');
            _stringBuilder.Append(name);
            _stringBuilder.Append(" : ");
            _stringBuilder.AppendLine(value.ToString());
        }
    }
}
