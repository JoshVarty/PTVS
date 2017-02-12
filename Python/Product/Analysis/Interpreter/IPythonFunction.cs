using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents an object which is a function.  Provides documentation for signature help.
    /// </summary>
    public interface IPythonFunction : IMember {
        string Name {
            get;
        }

        string Documentation {
            get;
        }

        bool IsBuiltin {
            get;            
        }
        
        /// <summary>
        /// False if binds instance when in a class, true if always static.
        /// </summary>
        bool IsStatic {
            get;
        }

        bool IsClassMethod {
            get;
        }

        IList<IPythonFunctionOverload> Overloads {
            get;
        }

        IPythonType DeclaringType {
            get;
        }

        IPythonModule DeclaringModule {
            get;
        }
    }
}
