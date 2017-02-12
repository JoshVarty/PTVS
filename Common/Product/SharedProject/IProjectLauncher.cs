using System.Collections.Generic;

namespace Microsoft.VisualStudioTools.Project {
    /// <summary>
    /// Defines an interface for launching a project or a file with or without debugging.
    /// </summary>
    public interface IProjectLauncher {
        /// <summary>
        /// Starts a project with or without debugging.
        /// </summary>
        /// <returns>HRESULT indicating success or failure.</returns>
        int LaunchProject(bool debug);

        /// <summary>
        /// Starts a file in a project with or without debugging.
        /// </summary>
        /// <returns>HRESULT indicating success or failure.</returns>
        int LaunchFile(string file, bool debug);
    }

    public interface IProjectLauncher2 : IProjectLauncher {
        /// <summary>
        /// Starts a file in a project with custom settings.
        /// </summary>
        int LaunchFile(string file, bool debug, IProjectLaunchProperties properties);
    }
}
