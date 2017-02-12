using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PythonTools.Interpreter.Default {
    class AnalysisOnlyInterpreterFactory : PythonInterpreterFactoryWithDatabase {
        readonly IEnumerable<string> _actualDatabasePaths;
        readonly PythonTypeDatabase _actualDatabase;

        private readonly static InterpreterFactoryCreationOptions CreationOptions = new InterpreterFactoryCreationOptions {
            WatchFileSystem = false
        };

        public AnalysisOnlyInterpreterFactory(Version version, string description = null)
            : base(GetConfiguration(version), CreationOptions) { }

        public AnalysisOnlyInterpreterFactory(Version version, IEnumerable<string> databasePaths, string description = null)
            : base(GetConfiguration(version, databasePaths?.ToArray() ?? Array.Empty<string>()), CreationOptions) {
            _actualDatabasePaths = databasePaths?.ToList();
        }

        public AnalysisOnlyInterpreterFactory(Version version, PythonTypeDatabase database, string description = null)
            : base(GetConfiguration(version, database.DatabaseDirectory), CreationOptions) {
            _actualDatabase = database;
        }

        private static InterpreterConfiguration GetConfiguration(Version version, params string[] databasePaths) {
            return new InterpreterConfiguration(
                "AnalysisOnly|" + version.ToString() + "|" + String.Join("|", databasePaths), 
                string.Format("Analysis {0}", version),
                null,
                null,
                null,
                null,
                InterpreterArchitecture.Unknown,
                version,
                InterpreterUIMode.SupportsDatabase
            );
        }

        private static string GetDescription(Version version, string description) {
            return description ?? string.Format("Python {0} Analyzer", version);
        }

        public override PythonTypeDatabase MakeTypeDatabase(string databasePath, bool includeSitePackages = true) {
            if (_actualDatabase != null) {
                return _actualDatabase;
            } else if (_actualDatabasePaths != null) {
                return new PythonTypeDatabase(this, _actualDatabasePaths);
            } else {
                return PythonTypeDatabase.CreateDefaultTypeDatabase(this);
            }
        }
    }
}
