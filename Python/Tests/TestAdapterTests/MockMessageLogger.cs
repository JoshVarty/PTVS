using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace TestAdapterTests {
    class MockMessageLogger : IMessageLogger {
        public readonly List<Tuple<TestMessageLevel, string>> Messages = new List<Tuple<TestMessageLevel, string>>();

        public void SendMessage(TestMessageLevel testMessageLevel, string message) {
            this.Messages.Add(new Tuple<TestMessageLevel, string>(testMessageLevel, message));
        }
    }
}
