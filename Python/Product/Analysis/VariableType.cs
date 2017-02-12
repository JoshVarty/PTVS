namespace Microsoft.PythonTools.Analysis {
    public enum VariableType {
        None,
        /// <summary>
        /// A parameter to a function definition or assignment to a member or global.
        /// </summary>
        Definition,

        /// <summary>
        /// A read from a global, local, member variable.
        /// </summary>
        Reference,

        /// <summary>
        /// A reference to a value which is passed into a parameter.
        /// </summary>
        Value
    }
}
