using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents a Python module which members can be imported from.
    /// </summary>
    public interface IPythonModule : IMemberContainer, IMember {
        string Name {
            get;
        }

        IEnumerable<string> GetChildrenModules();

        void Imported(IModuleContext context);

        /// <summary>
        /// The documentation of the module
        /// 
        /// New in 1.1.
        /// </summary>
        string Documentation {
            get;
        }
    }
}
