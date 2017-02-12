using System;

namespace Microsoft.PythonTools.Interpreter.Default {
    class CPythonInterpreterFactory : PythonInterpreterFactoryWithDatabase {
        public CPythonInterpreterFactory(InterpreterConfiguration configuration, InterpreterFactoryCreationOptions options) :
            base(configuration, options) { }
    }
}
