using System;

namespace Microsoft.CookiecutterTools.Telemetry {
    /// <summary>
    /// Represents telemetry operations in cookiecutter.
    /// </summary>
    internal sealed class CookiecutterTelemetry : ICookiecutterTelemetry {
        public static ICookiecutterTelemetry Current { get; set; }

        /// <summary>
        /// Area names show up as part of telemetry event names like:
        ///   VS/CookiecutterTools/[area]/[event]
        /// </summary>
        internal static class TelemetryArea {
            public const string Prereqs = "Prereqs";
            public const string Search = "Search";
            public const string Template = "Template";
        }

        internal class PrereqsEvents {
            public const string Python = "Python";
            public const string Install = "Install";
        }

        internal class SearchEvents {
            public const string Load = "Load";
            public const string More = "More";
            public const string CheckUpdate = "CheckUpdate";
        }

        internal class TemplateEvents {
            public const string Clone = "Clone";
            public const string Load = "Load";
            public const string Run = "Run";
            public const string Delete = "Delete";
            public const string Update = "Update";
            public const string AddToProject = "AddToProject";
        }

        public static void Initialize(ITelemetryService service = null) {
            if (Current == null) {
                Current = new CookiecutterTelemetry(service);
            }
        }

        public CookiecutterTelemetry(ITelemetryService service = null) {
            TelemetryService = service ?? VsTelemetryService.Current;
        }

        public ITelemetryService TelemetryService { get; }

        public void Dispose() {
            var disp = TelemetryService as IDisposable;
            disp?.Dispose();
        }
    }
}
