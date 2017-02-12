using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CookiecutterTests {
    [TestClass]
    public class TelemetryTestServiceTests {
        [TestMethod]
        public void TelemetryTestService_DefaultPrefixConstructorTest() {
            var telemetryService = new TelemetryTestService();
            Assert.AreEqual(TelemetryTestService.EventNamePrefixString, telemetryService.EventNamePrefix);
            Assert.AreEqual(TelemetryTestService.PropertyNamePrefixString, telemetryService.PropertyNamePrefix);
        }

        [TestMethod]
        public void TelemetryTestService_CustomPrefixConstructorTest() {
            var eventPrefix = "Event/Prefix/";
            var propertyPrefix = "Property.Prefix.";

            var telemetryService = new TelemetryTestService(eventPrefix, propertyPrefix);
            Assert.AreEqual(eventPrefix, telemetryService.EventNamePrefix);
            Assert.AreEqual(propertyPrefix, telemetryService.PropertyNamePrefix);
        }

        [TestMethod]
        public void TelemetryTestService_SimpleEventTest() {
            var area = "Options";
            var eventName = "event";

            var telemetryService = new TelemetryTestService();
            telemetryService.ReportEvent(area, eventName);
            string log = telemetryService.SessionLog;
            Assert.AreEqual(TelemetryTestService.EventNamePrefixString + area.ToString() + "/" + eventName + "\r\n", log);
        }

        [TestMethod]
        public void TelemetryTestService_EventWithParametersTest() {
            var area = "Options";
            var eventName = "event";

            var telemetryService = new TelemetryTestService();
            telemetryService.ReportEvent(area, eventName, new { parameter = "value" });
            string log = telemetryService.SessionLog;
            Assert.AreEqual(TelemetryTestService.EventNamePrefixString + area.ToString() + "/" + eventName +
                            "\r\n\t" + TelemetryTestService.PropertyNamePrefixString + area.ToString() + ".parameter : value\r\n", log);
        }
    }
}
