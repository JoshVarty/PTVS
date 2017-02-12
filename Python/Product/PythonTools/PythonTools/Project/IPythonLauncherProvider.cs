
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    public interface IPythonLauncherProvider {

        /// <summary>
        /// Gets the options for the provided launcher.
        /// </summary>
        /// <returns></returns>
        IPythonLauncherOptions GetLauncherOptions(IPythonProject properties);

        /// <summary>
        /// Gets the canonical name of the launcher.
        /// </summary>
        /// <remarks>
        /// This name is used to reference the launcher from project files and
        /// should not vary with language or culture. To specify a culture-
        /// sensitive name, use
        /// <see cref="IPythonLauncherProvider2.LocalizedName"/>.
        /// </remarks>
        string Name {
            get;
        }

        /// <summary>
        /// Gets a longer description of the launcher.
        /// </summary>
        string Description {
            get;
        }

        IProjectLauncher CreateLauncher(IPythonProject project);
    }

    public interface IPythonLauncherProvider2 : IPythonLauncherProvider {
        /// <summary>
        /// Gets the localized name of the launcher.
        /// </summary>
        string LocalizedName {
            get;
        }

        /// <summary>
        /// Gets the sort priority of the launcher. Lower values sort earlier in
        /// user-visible lists.
        /// </summary>
        int SortPriority {
            get;
        }
    }
}
