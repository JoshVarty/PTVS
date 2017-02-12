using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Interpreter {
    public sealed class LaunchConfiguration {
        private readonly InterpreterConfiguration _config;
        private readonly Dictionary<string, string> _options;

        public LaunchConfiguration(InterpreterConfiguration config, IDictionary<string, string> options = null) {
            _config = config;
            _options = options == null ?
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) :
                new Dictionary<string, string>(options, StringComparer.OrdinalIgnoreCase);
        }

        public LaunchConfiguration Clone(InterpreterConfiguration newConfig = null) {
            return new LaunchConfiguration(newConfig ?? _config, _options) {
                PreferWindowedInterpreter = PreferWindowedInterpreter,
                InterpreterPath = InterpreterPath,
                InterpreterArguments = InterpreterArguments,
                ScriptName = ScriptName,
                ScriptArguments = ScriptArguments,
                WorkingDirectory = WorkingDirectory,
                Environment = Environment != null ? new Dictionary<string, string>(Environment) : null,
                SearchPaths = SearchPaths?.ToList()
            };
        }

        public InterpreterConfiguration Interpreter => _config;

        public bool PreferWindowedInterpreter { get; set; }

        public string GetInterpreterPath() {
            if (!string.IsNullOrEmpty(InterpreterPath)) {
                return InterpreterPath;
            }

            if (_config == null) {
                return null;
            }

            if (PreferWindowedInterpreter && !string.IsNullOrEmpty(_config.WindowsInterpreterPath)) {
                return _config.WindowsInterpreterPath;
            }

            return _config.InterpreterPath;
        }

        public IEnumerable<KeyValuePair<string, string>> GetEnvironmentVariables() {
            return Environment.MaybeEnumerate();
        }

        public string InterpreterPath { get; set; }
        public string InterpreterArguments { get; set; }
        public string ScriptName { get; set; }
        public string ScriptArguments { get; set; }
        public string WorkingDirectory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<string> SearchPaths { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Dictionary<string, string> Environment { get; set; }

        public Dictionary<string, string> LaunchOptions => _options;

        public string GetLaunchOption(string option) {
            string value;
            if (!LaunchOptions.TryGetValue(option, out value)) {
                return null;
            }

            return value;
        }
    }
}
