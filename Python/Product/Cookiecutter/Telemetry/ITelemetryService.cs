namespace Microsoft.CookiecutterTools.Telemetry {
    /// <summary>
    /// Application telemetry service. In Visual Studio maps to IVsTelemetrySession.
    /// </summary>
    internal interface ITelemetryService {
        /// <summary>
        /// True of user opted in and telemetry is being collected
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Records event with parameters
        /// </summary>
        /// <param name="area">Telemetry area name such as 'Project'.</param>
        /// <param name="eventName">Event name.</param>
        /// <param name="parameters">
        /// Either string/object dictionary or anonymous
        /// collection of string/object pairs.
        /// </param>
        void ReportEvent(string area, string eventName, object parameters = null);
    }
}
