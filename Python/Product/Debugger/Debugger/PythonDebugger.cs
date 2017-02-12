using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.Debugger {
    class PythonDebugger {
        /// <summary>
        /// Creates a new PythonProcess object for debugging.  The process does not start until Start is called 
        /// on the returned PythonProcess object.
        /// </summary>
        public PythonProcess CreateProcess(PythonLanguageVersion langVersion, string exe, string args, string dir, string env, string interpreterOptions = null, PythonDebugOptions debugOptions = PythonDebugOptions.None) {
            return new PythonProcess(langVersion, exe, args, dir, env, interpreterOptions, debugOptions);
        }
    }
}
