using System;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Options {
    class InterpreterPlaceholder : IPythonInterpreterFactory {
        public const string PlaceholderId = "Placeholder";
        public InterpreterPlaceholder(string id, string description) {
            Configuration = new InterpreterConfiguration(
                PlaceholderId + ";" + id.ToString(),
                description,
                null,
                null,
                null,
                null,
                InterpreterArchitecture.Unknown,
                new Version(),
                InterpreterUIMode.Normal
            );
        }
        
        public InterpreterConfiguration Configuration { get; private set; }

        public Guid Id => Guid.Empty;
        public IPackageManager PackageManager => null;

        public IPythonInterpreter CreateInterpreter() {
            throw new NotSupportedException();
        }

        public IPythonInterpreterFactoryProvider Provider {
            get {
                throw new NotSupportedException();
            }
        }
    }
}
