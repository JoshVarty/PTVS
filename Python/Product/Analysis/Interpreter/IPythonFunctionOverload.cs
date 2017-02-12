using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents a single overload of a function.
    /// </summary>
    public interface IPythonFunctionOverload {
        string Documentation {
            get;
        }

        string ReturnDocumentation {
            get;
        }

        /// <summary>
        /// Shouldn't include hidden parameters (e.g. codeContext)
        /// </summary>
        IParameterInfo[] GetParameters();

        IList<IPythonType> ReturnType {
            get;
        }
    }
}
