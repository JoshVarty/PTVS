namespace Microsoft.CookiecutterTools.Model {
    class CookiecutterPythonInterpreter {
        public string InterpreterExecutablePath { get; }

        public CookiecutterPythonInterpreter(string interpreterExecutablePath) {
            InterpreterExecutablePath = interpreterExecutablePath;
        }
    }
}
