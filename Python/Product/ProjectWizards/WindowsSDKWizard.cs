using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.Win32;
using Project = EnvDTE.Project;
using ProjectItem = EnvDTE.ProjectItem;

namespace Microsoft.PythonTools.ProjectWizards {
    public sealed class WindowsSDKWizard : IWizard {
        public void ProjectFinishedGenerating(Project project) {}
        public void BeforeOpeningFile(ProjectItem projectItem) { }
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }
        public void RunFinished() { }

        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams
        ) {
            string winSDKVersion = string.Empty;
            try {
                string keyValue = string.Empty;
                // Attempt to get the installation folder of the Windows 10 SDK
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows Kits\Installed Roots");
                if (null != key) {
                    keyValue = (string)key.GetValue("KitsRoot10") + "Include";
                }
                // Get the latest SDK version from the name of the directory in the Include path of the SDK installation.
                if (!string.IsNullOrEmpty(keyValue)) {
                    string dirName = Directory.GetDirectories(keyValue, "10.*").OrderByDescending(x => x).FirstOrDefault();
                    winSDKVersion = Path.GetFileName(dirName);
                }
            } catch(Exception ex) {
                if (ex.IsCriticalException()) {
                    throw;
                }
            }
            
            if(string.IsNullOrEmpty(winSDKVersion)){
                winSDKVersion = "10.0.0.0"; // Default value to put in project file
            }

            replacementsDictionary.Add("$winsdkversion$", winSDKVersion);
            replacementsDictionary.Add("$winsdkminversion$", winSDKVersion);
        }

        public bool ShouldAddProjectItem(string filePath) {
            return true;
        }
    }
}
