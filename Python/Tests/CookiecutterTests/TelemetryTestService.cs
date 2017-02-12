using Microsoft.CookiecutterTools.Telemetry;

namespace CookiecutterTests {
    internal sealed class TelemetryTestService : TelemetryServiceBase, ITelemetryTestSupport {
        public static readonly string EventNamePrefixString = "Test/Cookiecutter/";
        public static readonly string PropertyNamePrefixString = "Test.Cookiecutter.";

        public TelemetryTestService(string eventNamePrefix, string propertyNamePrefix) :
            base(eventNamePrefix, propertyNamePrefix, new TestTelemetryRecorder()) {
        }

        public TelemetryTestService() :
            this(TelemetryTestService.EventNamePrefixString, TelemetryTestService.PropertyNamePrefixString) {
        }

        #region ITelemetryTestSupport
        public string SessionLog {
            get {
                ITelemetryTestSupport testSupport = this.TelemetryRecorder as ITelemetryTestSupport;
                return testSupport.SessionLog;
            }
        }

        public void Reset() {
            ITelemetryTestSupport testSupport = this.TelemetryRecorder as ITelemetryTestSupport;
            testSupport.Reset();
        }
        #endregion
    }
}
