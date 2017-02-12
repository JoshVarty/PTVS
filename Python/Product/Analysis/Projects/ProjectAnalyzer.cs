using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.Projects {
    public abstract class ProjectAnalyzer {
        /// <summary>
        /// Registers a new extension with the project analyzer, looking the DLL from the specified
        /// path and making available any exported IAnalysisExtensions.
        /// </summary>
        /// <param name="path"></param>
        public abstract void RegisterExtension(string path);

        /// <summary>
        /// Sends a command to an analysis extension with the specified input and returns
        /// the result.
        /// </summary>
        /// <param name="extensionName">The name of the analysis extension, as exported with
        /// AnalysisExtensionNameAttribute.</param>
        /// <param name="commandId">The command that the extension supports and will execute.</param>
        /// <param name="body">The input to the command.</param>
        /// <returns></returns>
        public abstract Task<string> SendExtensionCommandAsync(string extensionName, string commandId, string body);

        /// <summary>
        /// Raised when the analysis is complete for the specified file.
        /// </summary>
        public abstract event EventHandler<AnalysisCompleteEventArgs> AnalysisComplete;

        /// <summary>
        /// Gets the list of files which are being analyzed by this ProjectAnalyzer.
        /// </summary>
        public abstract IEnumerable<string> Files {
            get;
        }
    }
}
