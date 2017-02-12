using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// A PythonType which supports a super set of normal Python type operations.
    /// </summary>
    public interface IAdvancedPythonType : IPythonType {
        bool IsArray {
            get;
        }

        IPythonType GetElementType();

        IList<IPythonType> GetTypesPropagatedOnCall();

        bool IsGenericTypeDefinition {
            get;
        }

        IPythonType MakeGenericType(IPythonType[] indexTypes);
    }
}
