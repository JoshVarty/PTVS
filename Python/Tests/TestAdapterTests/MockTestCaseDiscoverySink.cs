using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace TestAdapterTests {
    class MockTestCaseDiscoverySink : ITestCaseDiscoverySink {
        public readonly List<TestCase> Tests = new List<TestCase>();

        #region ITestCaseDiscoverySink Members

        public void SendTestCase(TestCase discoveredTest) {
            this.Tests.Add(discoveredTest);
        }

        #endregion
    }
}
