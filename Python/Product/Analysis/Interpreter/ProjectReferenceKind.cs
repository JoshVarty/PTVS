
namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Specifies the kind of reference.  Currently we support references to .NET
    /// assemblies for IronPython and .pyds for C Python.
    /// </summary>
    public enum ProjectReferenceKind {
        None,
        /// <summary>
        /// The reference is to a .NET assembly.  The name is a fully qualified path to
        /// the assembly.
        /// </summary>
        Assembly,
        /// <summary>
        /// The reference is to a Python extension module.  The name is a fully qualified
        /// path to the .pyd file.
        /// </summary>
        ExtensionModule
    }
}
