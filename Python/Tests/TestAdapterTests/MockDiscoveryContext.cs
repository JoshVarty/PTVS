using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace TestAdapterTests {
    class MockDiscoveryContext : IDiscoveryContext {
        private readonly IRunSettings _runSettings;

        public MockDiscoveryContext(IRunSettings runSettings) {
            _runSettings = runSettings;
        }

        public IRunSettings RunSettings {
            get { return _runSettings; }
        }
    }
}
