using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PythonTools.Analysis;

namespace Microsoft.PythonTools.Projects {
    /// <summary>
    /// Provides an extension which registers against a given analyzer.
    /// </summary>
    public interface IAnalysisExtension {
        /// <summary>
        /// Called when the extension is registered for an analyzer.
        /// </summary>
        /// <param name="analyzer"></param>
        void Register(PythonAnalyzer analyzer);

        /// <summary>
        /// Handles an extension command.  The extension receives the command body and
        /// returns a response.
        /// </summary>
        string HandleCommand(string commandId, string body);
    }
}
