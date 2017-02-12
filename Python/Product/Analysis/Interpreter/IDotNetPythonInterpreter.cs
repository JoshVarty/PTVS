using System;

namespace Microsoft.PythonTools.Interpreter {
    public interface IDotNetPythonInterpreter {
        /// <summary>
        /// Gets the IPythonType object for the specifed .NET type;
        /// </summary>
        IPythonType GetBuiltinType(Type type);
    }
}
