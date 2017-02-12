using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Telemetry;

namespace Microsoft.CookiecutterTools.Telemetry {
    /// <summary>
    /// Implements telemetry recording in Visual Studio environment
    /// </summary>
    internal sealed class VsTelemetryRecorder : ITelemetryRecorder {
        private readonly TelemetrySession _session;
        private static readonly Lazy<VsTelemetryRecorder> _instance = new Lazy<VsTelemetryRecorder>(() => new VsTelemetryRecorder());

        private VsTelemetryRecorder() {
            _session = TelemetryService.DefaultSession;
        }

        public static ITelemetryRecorder Current => _instance.Value;

        #region ITelemetryRecorder
        /// <summary>
        /// True if telemetry is actually being recorder
        /// </summary>
        public bool IsEnabled  => _session.IsOptedIn;
        public bool CanCollectPrivateInformation => _session.CanCollectPrivateInformation;

        /// <summary>
        /// Records event with parameters
        /// </summary>
        public void RecordEvent(string eventName, object parameters = null) {
            if (this.IsEnabled) {
                TelemetryEvent telemetryEvent = new TelemetryEvent(eventName);
                if (parameters != null) {
                    var stringParameter = parameters as string;
                    if (stringParameter != null) {
                        telemetryEvent.Properties["Value"] = stringParameter;
                    } else {
                        IDictionary<string, object> dict = DictionaryExtension.FromAnonymousObject(parameters);
                        foreach (KeyValuePair<string, object> kvp in dict) {
                            telemetryEvent.Properties[kvp.Key] = kvp.Value;
                        }
                    }
                }
                _session.PostEvent(telemetryEvent);
            }
        }

        #endregion

        public void Dispose() { }
    }
}
