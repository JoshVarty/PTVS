using System;
using System.Windows.Forms;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    public interface IPythonLauncherOptions {
        /// <summary>
        /// Saves the current launcher options to storage.
        /// </summary>
        void SaveSettings();

        /// <summary>
        /// Loads the current launcher options from storage.
        /// </summary>
        void LoadSettings();

        /// <summary>
        /// Called when a setting has changed which the launcher may want to update to.
        /// </summary>
        void ReloadSetting(string settingName);

        /// <summary>
        /// Provides a notification that the launcher options have been altered but not saved or
        /// are now committed to disk.
        /// </summary>
        event EventHandler<DirtyChangedEventArgs> DirtyChanged;

        /// <summary>
        /// Gets a win forms control which allow editing of the options.
        /// </summary>
        Control Control {
            get;
        }
    }
}
