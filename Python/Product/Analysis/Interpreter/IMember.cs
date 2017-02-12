
namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents a member that appears in a module, type, etc...
    /// </summary>
    public interface IMember {
        PythonMemberType MemberType {
            get;
        }
    }
}
