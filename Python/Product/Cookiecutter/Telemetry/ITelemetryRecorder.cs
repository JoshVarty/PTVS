using System;

namespace Microsoft.CookiecutterTools.Telemetry {
    /// <summary>
    /// Represents object that records telemetry events and is called by
    /// the telemetry service. In Visual Studio environment maps to IVsTelemetryService
    /// whereas in tests can be replaced by an object that writes events to a string.
    /// </summary>
    internal interface ITelemetryRecorder : IDisposable {
        /// <summary>
        /// True if telemetry is actually being recorded
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Indicates if telemetry can collect private information
        /// </summary>
        bool CanCollectPrivateInformation { get; }

        /// <summary>
        /// Records event with parameters. Perameters are
        /// a collection of string/object pairs.
        /// </summary>
        void RecordEvent(string eventName, object parameters = null);
    }
}
