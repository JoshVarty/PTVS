using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Interpreter;

namespace TestUtilities.Python {
    public class MockPythonInterpreter : IPythonInterpreter {
        public readonly Dictionary<string, IPythonModule> _modules;
        public readonly HashSet<string> _moduleNames;
        public bool IsDatabaseInvalid;

        public MockPythonInterpreter(IPythonInterpreterFactory factory) {
            _modules = new Dictionary<string, IPythonModule>();
            _moduleNames = new HashSet<string>(StringComparer.Ordinal);
        }

        public void Dispose() { }

        public void AddModule(string name, IPythonModule module) {
            _modules[name] = module;
            ModuleNamesChanged?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// Removes a module. If <c>retainName</c> is true, keeps returning
        /// the module name from <see cref="GetModuleNames"/>.
        /// </summary>
        public void RemoveModule(string name, bool retainName = false) {
            if (retainName) {
                _moduleNames.Add(name);
            }
            if (_modules.Remove(name)) {
                ModuleNamesChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Initialize(PythonAnalyzer state) { }

        public IPythonType GetBuiltinType(BuiltinTypeId id) {
            throw new KeyNotFoundException();
        }

        public IList<string> GetModuleNames() {
            return _modules.Keys.Concat(_moduleNames).ToArray();
        }

        public event EventHandler ModuleNamesChanged;

        public IPythonModule ImportModule(string name) {
            IPythonModule res;
            _modules.TryGetValue(name, out res);
            return res;
        }

        public IModuleContext CreateModuleContext() {
            throw new NotImplementedException();
        }

        public Task AddReferenceAsync(ProjectReference reference, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public void RemoveReference(ProjectReference reference) {
            throw new NotImplementedException();
        }
    }
}
