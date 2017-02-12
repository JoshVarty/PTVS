using EnvDTE;
using Microsoft.PythonTools.Uwp.Interpreter;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Microsoft.PythonTools.Uwp.Wizards {
    public sealed class PythonUwpSdkWizard : IWizard {
        public void ProjectFinishedGenerating(EnvDTE.Project project) { }
        public void BeforeOpeningFile(ProjectItem projectItem) { }
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }
        public void RunFinished() { }

        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams
        ) {
            // Pick the largest installed version
            var version = InstalledPythonUwpInterpreter.GetInterpreters().Max(x => x.Key);

            if (version == null) {
                // Show an error dialog if CPython UWP SDK is not installed
                MessageBox.Show(Resources.PythonUwpSdkNotFound, Resources.PTVSDialogBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                throw new WizardCancelledException(Resources.PythonUwpSdkNotFound);
            }

            replacementsDictionary.Add("$pythonuwpsdkversion$", version.ToString());
        }

        public bool ShouldAddProjectItem(string filePath) {
            return true;
        }
    }
}

