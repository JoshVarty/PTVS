
namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents a built-in Python module.  The built-in module needs to respond to
    /// some extra requests for members by name which supports getting hidden members
    /// such as "NoneType" which logically live in the built-in module but don't actually
    /// exist there by name.
    /// 
    /// The full list of types which will be accessed through GetAnyMember but don't exist
    /// in the built-in module includes:
    ///     NoneType
    ///     generator
    ///     builtin_function
    ///     builtin_method_descriptor
    ///     function
    ///     ellipsis
    ///     
    /// These are the addition types in BuiltinTypeId which do not exist in __builtin__.
    /// </summary>
    public interface IBuiltinPythonModule : IPythonModule {
        IMember GetAnyMember(string name);
    }
}
