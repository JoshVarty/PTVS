using Microsoft.PythonTools.Interpreter;
using System.IO;

namespace Microsoft.PythonTools.Uwp.Interpreter {
    class PythonUwpInterpreterFactory : PythonInterpreterFactoryWithDatabase {
        private static readonly InterpreterFactoryCreationOptions CreationOptions = new InterpreterFactoryCreationOptions {
            PackageManager = BuiltInPackageManagers.Pip,
            WatchFileSystem = true
        };

        public PythonUwpInterpreterFactory(InterpreterConfiguration configuration) 
            : base(configuration, new InterpreterFactoryCreationOptions {
                PackageManager = BuiltInPackageManagers.Pip,
                DatabasePath = Path.Combine(configuration.PrefixPath, "completionDB"),
                WatchFileSystem = true
            }) {
        }

        public override IPythonInterpreter MakeInterpreter(PythonInterpreterFactoryWithDatabase factory) {
            return new PythonUwpInterpreter(factory);
        }
    }
}
