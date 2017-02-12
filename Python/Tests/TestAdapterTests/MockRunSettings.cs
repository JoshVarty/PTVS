using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace TestAdapterTests {
    class MockRunSettings : IRunSettings {
        private readonly string _xml;

        public MockRunSettings(string xml) {
            _xml = xml;
        }

        public string SettingsXml {
            get {
                return _xml;
            }
        }

        public ISettingsProvider GetSettings(string settingsName) {
            throw new NotImplementedException();
        }
    }
}
