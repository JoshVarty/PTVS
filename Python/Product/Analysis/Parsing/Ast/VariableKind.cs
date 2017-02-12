namespace Microsoft.PythonTools.Parsing.Ast {
    internal enum VariableKind {

        /// <summary>
        /// Local variable.
        /// 
        /// Local variables can be referenced from nested lambdas
        /// </summary>
        Local,

        /// <summary>
        /// Parameter to a LambdaExpression
        /// 
        /// Like locals, they can be referenced from nested lambdas
        /// </summary>
        Parameter,

        /// <summary>
        /// Global variable
        /// 
        /// Should only appear in global (top level) lambda.
        /// </summary>
        Global,

        /// <summary>
        /// Reference a variable that is declared in an outer scope
        /// </summary>
        Nonlocal
    }
}