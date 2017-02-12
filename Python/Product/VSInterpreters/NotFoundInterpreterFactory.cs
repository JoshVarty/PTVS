using System;
using System.Collections.Generic;
using Microsoft.PythonTools.Analysis;
using System.Reflection;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Interpreter
{
    sealed class NotFoundInterpreter : IPythonInterpreter {
        public void Dispose() { }
        public void Initialize(PythonAnalyzer state) { }
        public IPythonType GetBuiltinType(BuiltinTypeId id) { throw new KeyNotFoundException(); }
        public IList<string> GetModuleNames() { return new string[0]; }
        public event EventHandler ModuleNamesChanged { add { } remove { } }
        public IPythonModule ImportModule(string name) { return null; }
        public IModuleContext CreateModuleContext() { return null; }
    }

    sealed public class NotFoundInterpreterFactory : IPythonInterpreterFactory {
        public NotFoundInterpreterFactory(
            string id,
            Version version,
            string description = null,
            string prefixPath = null,
            InterpreterArchitecture architecture = default(InterpreterArchitecture),
            string descriptionSuffix = null) {
            Configuration = new InterpreterConfiguration(
                id,
                description ?? "Unknown Python {0}{1: ()} (unavailable)".FormatUI(version, architecture),
                prefixPath,
                null,
                null,
                null,
                architecture,
                version,
                InterpreterUIMode.CannotBeDefault | InterpreterUIMode.CannotBeConfigured
            );
        }

        public string Description { get; private set; }
        public InterpreterConfiguration Configuration { get; private set; }
        public Guid Id { get; private set; }
        public IPackageManager PackageManager => null;

        public IPythonInterpreter CreateInterpreter() {
            return new NotFoundInterpreter();
        }
    }
}
