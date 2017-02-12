using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PythonTools.Options;

namespace TestUtilities.Python {
    public sealed class MockPythonToolsOptionsService : IPythonToolsOptionsService {
        private Dictionary<string, Dictionary<string, string>> _options = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        public void SaveString(string name, string category, string value) {
            Dictionary<string, string> catDict;
            if (!_options.TryGetValue(category, out catDict)) {
                _options[category] = catDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
            catDict[name] = value;
        }

        public string LoadString(string name, string category) {
            Dictionary<string, string> catDict;
            string res;
            if (!_options.TryGetValue(category, out catDict) ||
                !catDict.TryGetValue(name, out res)) {
                return null;
            }

            return res;
        }

        public void DeleteCategory(string category) {
            foreach (var key in _options.Keys.Where(k => k.StartsWith(category + "\\") || k == category).ToList()) {
                _options.Remove(key);
            }
        }
    }
}
