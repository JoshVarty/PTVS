using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents an object which can contain other members.
    /// </summary>
    public interface IMemberContainer {
        IMember GetMember(IModuleContext context, string name);
        IEnumerable<string> GetMemberNames(IModuleContext moduleContext);
    }
}
