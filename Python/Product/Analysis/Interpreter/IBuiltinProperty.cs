
namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents a built-in property which has a getter/setter.  
    /// </summary>
    public interface IBuiltinProperty : IMember {
        /// <summary>
        /// The type of the value the property gets/sets.
        /// </summary>
        IPythonType Type {
            get;
        }

        /// <summary>
        /// True if the property is static (declared on the class) not the instance.
        /// </summary>
        bool IsStatic {
            get;
        }

        /// <summary>
        /// Documentation for the property.
        /// </summary>
        string Documentation {
            get;
        }

        /// <summary>
        /// A user readable description of the property.
        /// </summary>
        string Description {
            get;
        }
    }
}
