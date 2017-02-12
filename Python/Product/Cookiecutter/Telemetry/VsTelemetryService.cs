using System;

namespace Microsoft.CookiecutterTools.Telemetry {

    internal sealed class VsTelemetryService : TelemetryServiceBase, ITelemetryLog {
        public static readonly string EventNamePrefixString = "VS/Cookiecutter/";
        public static readonly string PropertyNamePrefixString = "VS.Cookiecutter.";

        private static readonly Lazy<VsTelemetryService> _instance = new Lazy<VsTelemetryService>(() => new VsTelemetryService());

        public VsTelemetryService()
            : base(VsTelemetryService.EventNamePrefixString, VsTelemetryService.PropertyNamePrefixString, VsTelemetryRecorder.Current) {
            }

        public static TelemetryServiceBase Current => _instance.Value;

        #region ITelemetryLog
        public string SessionLog {
            get { return (base.TelemetryRecorder as ITelemetryLog)?.SessionLog; }
        }

        public void Reset() {
            (base.TelemetryRecorder as ITelemetryLog)?.Reset();
        }
        #endregion
    }
}
