using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.Telemetry;

namespace Microsoft.PythonTools.Logging {
    /// <summary>
    /// Implements telemetry recording in Visual Studio environment
    /// </summary>
    [Export(typeof(IPythonToolsLogger))]
    internal sealed class VsTelemetryLogger : IPythonToolsLogger {
        private readonly Lazy<TelemetrySession> _session = new Lazy<TelemetrySession>(() => TelemetryService.DefaultSession);

        private const string EventPrefix = "vs/python/";
        private const string PropertyPrefix = "VS.Python.";

        public void LogEvent(PythonLogEvent logEvent, object argument) {
            var session = _session.Value;
            // No session is not a fatal error
            if (session == null) {
                return;
            }

            // Never send events when users have not opted in.
            if (!session.IsOptedIn) {
                return;
            }

            // Certain events are not collected
            switch (logEvent) {
                case PythonLogEvent.AnalysisWarning:
                case PythonLogEvent.AnalysisOperationFailed:
                case PythonLogEvent.AnalysisOperationCancelled:
                case PythonLogEvent.AnalysisExitedAbnormally:
                    return;
            }

            var evt = new TelemetryEvent(EventPrefix + logEvent.ToString());
            var props = PythonToolsLoggerData.AsDictionary(argument);
            if (props != null) {
                foreach (var kv in props) {
                    evt.Properties[PropertyPrefix + kv.Key] = kv.Value;
                }
            } else if (argument != null) {
                evt.Properties[PropertyPrefix + "Value"] = argument;
            }

            session.PostEvent(evt);
        }
    }
}
