using System.Threading.Tasks;
using Microsoft.PythonTools.Intellisense;
using Microsoft.PythonTools.InteractiveWindow;

namespace Microsoft.PythonTools.Repl {
    /// <summary>
    /// The Python repl evaluator.  An instance of this can be acquired by creating a REPL window
    /// via PythonToolsPackage.CreatePythonRepl and getting the Evaluator from the resulting
    /// window.
    /// 
    /// This interface provides additional functionality for interacting with the Python REPL
    /// above and beyond the standard IReplEvaluator interface.
    /// </summary>
    interface IPythonInteractiveEvaluator {
        /// <summary>
        /// Executes the specified file in the REPL window.
        /// 
        /// Does not reset the process, and the process will remain after the file is executed.
        /// </summary>
        Task<bool> ExecuteFileAsync(string filename, string extraArgs);

        /// <summary>
        /// Returns true if the REPL window process has exited.
        /// </summary>
        bool IsDisconnected { get; }

        /// <summary>
        /// Returns true if the REPL window is currently executing user code.
        /// </summary>
        bool IsExecuting { get; }

        /// <summary>
        /// User friendly name of the evaluator.
        /// </summary>
        string DisplayName { get; }
    }
}
