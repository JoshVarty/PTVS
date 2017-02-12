using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Projects {
    /// <summary>
    /// Provides information about a Ptyhon project.  This is an abstract base class that
    /// different project systems can implement.  Tools which want to plug in an extend the
    /// Python analysis system can work with the PythonProject to get information about
    /// the project.
    /// 
    /// This differs from the ProjectAnalyzer class in that it contains more rich information
    /// about the configuration of the project related to running and executing.
    /// </summary>
    public abstract class PythonProject {
        /// <summary>
        /// Gets a property for the project.  Users can get/set their own properties, also these properties
        /// are available:
        /// 
        ///     CommandLineArguments -> arguments to be passed to the debugged program.
        ///     InterpreterPath  -> gets a configured directory where the interpreter should be launched from.
        ///     IsWindowsApplication -> determines whether or not the application is a windows application (for which no console window should be created)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract string GetProperty(string name);

        public abstract string GetUnevaluatedProperty(string name);

        /// <summary>
        /// Sets a property for the project.  See GetProperty for more information on common properties.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetProperty(string name, string value);

        public abstract IPythonInterpreterFactory GetInterpreterFactory();


        public abstract ProjectAnalyzer Analyzer {
            get;
        }

        public abstract event EventHandler ProjectAnalyzerChanged;

        public abstract string ProjectHome { get; }

        public abstract LaunchConfiguration GetLaunchConfigurationOrThrow();
    }
}
