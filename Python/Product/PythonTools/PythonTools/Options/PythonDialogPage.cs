using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools.Options {
    /// <summary>
    /// Base class used for saving/loading of settings.  The settings are stored in VSRegistryRoot\PythonTools\Options\Category\SettingName
    /// where Category is provided in the constructor and SettingName is provided to each call of the Save*/Load* APIs.
    /// x = 42
    /// 
    /// The primary purpose of this class is so that we can be in control of providing reasonable default values.
    /// </summary>
    [ComVisible(true)]
    public class PythonDialogPage : DialogPage {
        internal PythonToolsService PyService {
            get {
                return ((IServiceProvider)Site).GetPythonToolsService();
            }
        }

        internal IComponentModel ComponentModel {
            get {
                return ((IServiceProvider)Site).GetComponentModel();
            }
        }
    }
}
