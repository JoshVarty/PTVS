using System.ComponentModel;

namespace Microsoft.PythonTools.Parsing {

    /// <summary>
    /// Defines a kind of the source code. The parser sets its initial state accordingly.
    /// </summary>
    enum SourceCodeKind {
        [EditorBrowsable(EditorBrowsableState.Never)]
        Unspecified = 0,

        /// <summary>
        /// The code is an expression.
        /// </summary>
        Expression = 1,

        /// <summary>
        /// The code is a sequence of statements.
        /// </summary>
        Statements = 2,

        /// <summary>
        /// The code is a single statement.
        /// </summary>
        SingleStatement = 3,

        /// <summary>
        /// The code is a content of a file.
        /// </summary>
        File = 4,

        /// <summary>
        /// The code is an interactive command.
        /// </summary>
        InteractiveCode = 5,

        /// <summary>
        /// The language parser auto-detects the kind. A syntax error is reported if it is not able to do so.
        /// </summary>
        AutoDetect = 6
    }
}