using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CookiecutterTests {
    [TestClass]
    public class StringTelemetryRecorderTests {
        [TestMethod]
        public void StringTelemetryRecorder_SimpleEventTest() {
            var eventName = "event";

            var telemetryRecorder = new TestTelemetryRecorder();
            telemetryRecorder.RecordEvent(eventName);

            string log = telemetryRecorder.SessionLog;
            Assert.AreEqual(eventName + "\r\n", log);
        }

        [TestMethod]
        public void StringTelemetryRecorder_EventWithDictionaryTest() {
            var eventName = "event";
            var parameter1 = "parameter1";
            var value1 = "value1";
            var parameter2 = "parameter2";
            var value2 = "value2";

            var telemetryRecorder = new TestTelemetryRecorder();
            telemetryRecorder.RecordEvent(eventName, new Dictionary<string, object>() { { parameter1, value1 }, { parameter2, value2 } });

            string log = telemetryRecorder.SessionLog;
            Assert.AreEqual(eventName + "\r\n\t" + parameter1 + " : " + value1 + "\r\n\t" + parameter2 + " : " + value2 + "\r\n", log);
        }

        [TestMethod]
        public void StringTelemetryRecorder_EventWithAnonymousCollectionTest() {
            var eventName = "event";

            var telemetryRecorder = new TestTelemetryRecorder();
            telemetryRecorder.RecordEvent(eventName, new { parameter1 = "value1", parameter2 = "value2" });

            string log = telemetryRecorder.SessionLog;
            Assert.AreEqual(eventName + "\r\n\tparameter1 : value1\r\n\tparameter2 : value2\r\n", log);
        }
    }
}
