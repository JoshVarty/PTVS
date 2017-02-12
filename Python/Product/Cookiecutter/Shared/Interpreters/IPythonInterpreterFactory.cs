using System;

namespace Microsoft.CookiecutterTools.Interpreters {
    /// <summary>
    /// Provides a factory for creating IPythonInterpreters for a specific
    /// Python implementation.
    /// 
    /// The factory includes information about what type of interpreter will be
    /// created - this is used for displaying information to the user and for
    /// tracking per-interpreter settings.
    /// 
    /// It also contains a method for creating an interpreter. This allows for
    /// stateful interpreters that participate in analysis or track other state.
    /// </summary>
    public interface IPythonInterpreterFactory {
        /// <summary>
        /// Configuration settings for the interpreter.
        /// </summary>
        InterpreterConfiguration Configuration {
            get;
        }
    }
}
