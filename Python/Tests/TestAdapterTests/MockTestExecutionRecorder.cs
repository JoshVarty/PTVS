using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace TestAdapterTests {
    class MockTestExecutionRecorder : IFrameworkHandle {
        public readonly List<TestResult> Results = new List<TestResult>();
        public readonly List<string> Messages = new List<string>();

        public bool EnableShutdownAfterTestRun {
            get {
                return false;
            }
            set {
            }
        }

        public int LaunchProcessWithDebuggerAttached(string filePath, string workingDirectory, string arguments, IDictionary<string, string> environmentVariables) {
            return 0;
        }

        public void RecordResult(TestResult result) {
            this.Results.Add(result);
        }

        public void RecordAttachments(IList<AttachmentSet> attachmentSets) {
        }

        public void RecordEnd(TestCase testCase, TestOutcome outcome) {
        }

        public void RecordStart(TestCase testCase) {
        }

        public void SendMessage(TestMessageLevel testMessageLevel, string message) {
            Messages.Add(string.Format("{0}:{1}", testMessageLevel, message));
        }
    }
}
