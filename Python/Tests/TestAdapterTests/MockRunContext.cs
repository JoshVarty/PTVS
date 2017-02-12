using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace TestAdapterTests {
    class MockRunContext : IRunContext {
        private readonly IRunSettings _runSettings;

        public MockRunContext(IRunSettings runSettings) {
            _runSettings = runSettings;
        }

        public ITestCaseFilterExpression GetTestCaseFilter(IEnumerable<string> supportedProperties, Func<string, TestProperty> propertyProvider) {
            throw new NotImplementedException();
        }

        public bool InIsolation {
            get { throw new NotImplementedException(); }
        }

        public bool IsBeingDebugged {
            get { return false; }
        }

        public bool IsDataCollectionEnabled {
            get { throw new NotImplementedException(); }
        }

        public bool KeepAlive {
            get { throw new NotImplementedException(); }
        }

        public string SolutionDirectory {
            get { throw new NotImplementedException(); }
        }

        public string TestRunDirectory {
            get { throw new NotImplementedException(); }
        }

        public IRunSettings RunSettings => _runSettings;
    }
}
