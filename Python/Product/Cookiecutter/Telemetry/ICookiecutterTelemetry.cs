using System;

namespace Microsoft.CookiecutterTools.Telemetry {
    /// <summary>
    /// Represents telemetry operations in cookiecutter.
    /// </summary>
    internal interface ICookiecutterTelemetry : IDisposable {
        ITelemetryService TelemetryService { get; }
    }
}
