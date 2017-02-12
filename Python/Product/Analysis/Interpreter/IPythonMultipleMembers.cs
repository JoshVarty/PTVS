using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents a collection of multiple members which can appear under a single name.
    /// </summary>
    public interface IPythonMultipleMembers : IMember {
        IList<IMember> Members {
            get;
        }
    }
}
