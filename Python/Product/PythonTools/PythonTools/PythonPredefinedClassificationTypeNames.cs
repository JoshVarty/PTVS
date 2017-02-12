
namespace Microsoft.PythonTools {
    public static class PythonPredefinedClassificationTypeNames {
        /// <summary>
        /// Open grouping classification.  Used for (, [, {, ), ], and }...  A subtype of the Python
        /// operator grouping.
        /// </summary>
        public const string Grouping = "Python grouping";
       
        /// <summary>
        /// Classification used for comma characters when used outside of a literal, comment, etc...
        /// </summary>
        public const string Comma = "Python comma";

        /// <summary>
        /// Classification used for . characters when used outside of a literal, comment, etc...
        /// </summary>
        public const string Dot = "Python dot";

        /// <summary>
        /// Classification used for all other operators
        /// </summary>
        public const string Operator = "Python operator";

        /// <summary>
        /// Classification used for classes/types.
        /// </summary>
        public const string Class = "Python class";

        /// <summary>
        /// Classification used for imported modules.
        /// </summary>
        public const string Module = "Python module";

        /// <summary>
        /// Classification used for functions.
        /// </summary>
        public const string Function = "Python function";

        /// <summary>
        /// Classification used for parameters.
        /// </summary>
        public const string Parameter = "Python parameter";

        /// <summary>
        /// Classification used for builtins.
        /// </summary>
        public const string Builtin = "Python builtin";
    }
}
