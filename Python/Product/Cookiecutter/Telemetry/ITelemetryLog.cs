namespace Microsoft.CookiecutterTools.Telemetry {
    /// <summary>
    /// Represent persistent telemetry log
    /// </summary>
    internal interface ITelemetryLog {
        /// <summary>
        /// Resets current session and clear telemetry log.
        /// </summary>
        void Reset();

        /// <summary>
        /// Returns current telemetry log as a string.
        /// </summary>
        string SessionLog { get; }
    }
}
