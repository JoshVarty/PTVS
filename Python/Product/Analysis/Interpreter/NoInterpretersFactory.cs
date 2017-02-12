using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Interpreter {
    sealed class NoInterpretersFactory : IPythonInterpreterFactory {
        public NoInterpretersFactory() {
            Configuration = new InterpreterConfiguration(
                InterpreterRegistryConstants.NoInterpretersFactoryId,
                Strings.NoInterpretersDescription,
                uiMode: InterpreterUIMode.CannotBeDefault | InterpreterUIMode.CannotBeConfigured
            );
        }

        public InterpreterConfiguration Configuration { get; }

        public IPackageManager PackageManager => null;

        public IPythonInterpreter CreateInterpreter() {
            return new NoInterpretersInterpreter(PythonTypeDatabase.CreateDefaultTypeDatabase());
        }
    }

    class NoInterpretersInterpreter : IPythonInterpreter {
        private readonly PythonTypeDatabase _database;

        public NoInterpretersInterpreter(PythonTypeDatabase database) {
            _database = database;
        }

        public void Dispose() { }

        public event EventHandler ModuleNamesChanged { add { } remove { } }

        public void Initialize(PythonAnalyzer state) { }
        public IModuleContext CreateModuleContext() => null;
        public IList<string> GetModuleNames() => _database.GetModuleNames().ToList();
        public IPythonModule ImportModule(string name) => _database.GetModule(name);

        public IPythonType GetBuiltinType(BuiltinTypeId id) {
            if (id == BuiltinTypeId.Unknown || _database.BuiltinModule == null) {
                return null;
            }
            var name = SharedDatabaseState.GetBuiltinTypeName(id, _database.LanguageVersion);
            var res = _database.BuiltinModule.GetAnyMember(name) as IPythonType;
            if (res == null) {
                throw new KeyNotFoundException(string.Format("{0} ({1})", id, (int)id));
            }
            return res;
        }
    }
}
