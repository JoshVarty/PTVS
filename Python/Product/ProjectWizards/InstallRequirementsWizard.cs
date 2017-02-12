using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.TemplateWizard;
using Project = EnvDTE.Project;
using ProjectItem = EnvDTE.ProjectItem;

namespace Microsoft.PythonTools.ProjectWizards {
    public sealed class InstallRequirementsWizard : IWizard {
        public void ProjectFinishedGenerating(Project project) {
            if (project.DTE.SuppressUI) {
                return;
            }

            ProjectItem requirementsTxt = null;
            try {
                requirementsTxt = project.ProjectItems.Item("requirements.txt");
            } catch (ArgumentException) {
            }

            if (requirementsTxt == null) {
                return;
            }

            var txt = requirementsTxt.FileNames[0];
            if (!File.Exists(txt)) {
                return;
            }

            var provider = WizardHelpers.GetProvider(project.DTE);
            if (provider == null) {
                return;
            }

            try {
                object inObj = (object)txt, outObj = null;
                project.DTE.Commands.Raise(
                    GuidList.guidPythonToolsCmdSet.ToString("B"),
                    (int)PkgCmdIDList.cmdidInstallProjectRequirements,
                    ref inObj,
                    ref outObj
                );
            } catch (Exception ex) {
                if (ex.IsCriticalException()) {
                    throw;
                }
                TaskDialog.ForException(
                    provider,
                    ex,
                    Strings.InstallRequirementsFailed,
                    Strings.IssueTrackerUrl
                ).ShowModal();
            }
        }

        public void BeforeOpeningFile(ProjectItem projectItem) { }
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }
        public void RunFinished() { }

        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams
        ) { }

        public bool ShouldAddProjectItem(string filePath) {
            return true;
        }
    }
}
