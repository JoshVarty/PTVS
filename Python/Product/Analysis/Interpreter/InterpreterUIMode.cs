using System;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Specifies the interpreter's behavior in the UI.
    /// </summary>
    /// <remarks>New in 2.2</remarks>
    [Flags]
    public enum InterpreterUIMode : int {
        /// <summary>
        /// Interpreter can be set or selected as the default, and is visible to
        /// the user.
        /// </summary>
        Normal = 0x00,

        /// <summary>
        /// Interpreter is not displayed in the user interface, but can still be
        /// added to a project if the ID is known.
        /// </summary>
        Hidden = 0x01,

        /// <summary>
        /// Interpreter cannot be selected as the default. Implies
        /// <see cref="CannotBeAutoDefault"/>.
        /// </summary>
        CannotBeDefault = 0x02,

        /// <summary>
        /// Interpreter cannot be automatically selected as the default.
        /// </summary>
        CannotBeAutoDefault = 0x04,

        /// <summary>
        /// Interpreter has no user-modifiable settings.
        /// </summary>
        CannotBeConfigured = 0x08,

        SupportsDatabase = 0x10,
    }
}
