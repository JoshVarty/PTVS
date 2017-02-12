using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Represents information about an individual parameter.  Used for providing
    /// signature help.
    /// </summary>
    public interface IParameterInfo {
        /// <summary>
        /// The name of the parameter.
        /// </summary>
        string Name {
            get;
        }

        /// <summary>
        /// The types of the parameter.
        /// </summary>
        IList<IPythonType> ParameterTypes {
            get;
        }

        /// <summary>
        /// Documentation for the parameter.
        /// </summary>
        string Documentation {
            get;
        }

        /// <summary>
        /// True if the parameter is a *args parameter.
        /// </summary>
        bool IsParamArray {
            get;
        }

        /// <summary>
        /// True if the parameter is a **args parameter.
        /// </summary>
        bool IsKeywordDict {
            get;
        }

        /// <summary>
        /// Default value.  Returns String.Empty for optional parameters, or a string representation of the default value
        /// </summary>
        string DefaultValue {
            get;
        }
    }
}
