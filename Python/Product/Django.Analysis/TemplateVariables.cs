using System.Collections.Generic;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Django.Analysis {
    class TemplateVariables {
        private readonly Dictionary<string, Dictionary<IPythonProjectEntry, ValuesAndVersion>> _values = new Dictionary<string, Dictionary<IPythonProjectEntry, ValuesAndVersion>>();

        public void UpdateVariable(string name, AnalysisUnit unit, IEnumerable<AnalysisValue> values) {
            Dictionary<IPythonProjectEntry, ValuesAndVersion> entryMappedValues;
            if (!_values.TryGetValue(name, out entryMappedValues)) {
                _values[name] = entryMappedValues = new Dictionary<IPythonProjectEntry, ValuesAndVersion>();
            }

            foreach (var value in values) {
                var module = value.DeclaringModule ?? unit.Project;
                ValuesAndVersion valsAndVersion;
                if (!entryMappedValues.TryGetValue(module, out valsAndVersion) || valsAndVersion.DeclaringVersion != module.AnalysisVersion) {
                    entryMappedValues[module] = valsAndVersion = new ValuesAndVersion(module.AnalysisVersion);
                }

                valsAndVersion.Values.Add(value);
            }
        }

        struct ValuesAndVersion {
            public readonly int DeclaringVersion;
            public readonly HashSet<AnalysisValue> Values;

            public ValuesAndVersion(int declaringVersion) {
                DeclaringVersion = declaringVersion;
                Values = new HashSet<AnalysisValue>();
            }
        }

        internal Dictionary<string, HashSet<AnalysisValue>> GetAllValues() {
            var res = new Dictionary<string, HashSet<AnalysisValue>>();

            foreach (var nameAndValues in _values) {
                HashSet<AnalysisValue> curValues = new HashSet<AnalysisValue>();
                res[nameAndValues.Key] = curValues;

                foreach (var projectAndValues in nameAndValues.Value) {
                    foreach (var analysisValue in projectAndValues.Value.Values) {
                        if (analysisValue.DeclaringModule == null ||
                            analysisValue.DeclaringModule.AnalysisVersion == projectAndValues.Value.DeclaringVersion) {
                            curValues.Add(analysisValue);
                        }
                    }
                }
            }
            return res;
        }
    }

}
