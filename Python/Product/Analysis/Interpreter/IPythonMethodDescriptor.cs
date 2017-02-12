
namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents a method descriptor for an instance of a function.
    /// </summary>
    public interface IPythonMethodDescriptor : IMember {
        /// <summary>
        /// The built-in function that the method descriptor wraps.
        /// </summary>
        IPythonFunction Function {
            get;
        }

        /// <summary>
        /// True if the method is already bound to an instance.
        /// </summary>
        bool IsBound {
            get;
        }
    }
}
