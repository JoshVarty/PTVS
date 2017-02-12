using System;
using System.IO;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonInterpreterFactory : PythonInterpreterFactoryWithDatabase {
        public IronPythonInterpreterFactory(InterpreterArchitecture arch)
            : base(GetConfiguration(arch), GetCreationOptions(arch)) { }

        private static string GetInterpreterId(InterpreterArchitecture arch) {
            if (arch == InterpreterArchitecture.x64) {
                return "IronPython|2.7-64";
            } else {
                return "IronPython|2.7-32";
            }
        }

        internal static InterpreterConfiguration GetConfiguration(InterpreterArchitecture arch) {
            var prefixPath = IronPythonResolver.GetPythonInstallDir();
            if (string.IsNullOrEmpty(prefixPath)) {
                return null;
            }

            return new InterpreterConfiguration(
                GetInterpreterId(arch),
                string.Format("IronPython 2.7{0: ()}", arch),
                prefixPath,
                Path.Combine(prefixPath, arch == InterpreterArchitecture.x64 ? "ipy64.exe" : "ipy.exe"),
                Path.Combine(prefixPath, arch == InterpreterArchitecture.x64 ? "ipyw64.exe" : "ipyw.exe"),
                "IRONPYTHONPATH",
                arch,
                new Version(2, 7),
                InterpreterUIMode.SupportsDatabase
            );
        }

        private static InterpreterFactoryCreationOptions GetCreationOptions(InterpreterArchitecture arch) {
            return new InterpreterFactoryCreationOptions {
                PackageManager = BuiltInPackageManagers.PipXFrames,
                DatabasePath = Path.Combine(
                    PythonTypeDatabase.CompletionDatabasePath,
                    InterpreterFactoryCreator.GetRelativePathForConfigurationId(GetInterpreterId(arch))
                )
            };
        }

        public override IPythonInterpreter MakeInterpreter(PythonInterpreterFactoryWithDatabase factory) {
            return new IronPythonInterpreter(factory);
        }
    }
}
