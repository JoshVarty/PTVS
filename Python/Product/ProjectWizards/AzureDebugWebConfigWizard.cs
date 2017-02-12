using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.TemplateWizard;

namespace Microsoft.PythonTools.ProjectWizards {
    public sealed class AzureDebugWebConfigWizard : IWizard {
        public void BeforeOpeningFile(ProjectItem projectItem) { }

        public void ProjectFinishedGenerating(Project project) { }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem) {
            if (!projectItem.Name.Equals("web.debug.config", StringComparison.OrdinalIgnoreCase)) {
                return;
            }

            var projectDir = PathUtils.GetParent(projectItem.FileNames[0]);

            // Also copy Microsoft.PythonTools.WebRole.dll and ptvsd into the project
            var ptvsdSource = PythonToolsInstallPath.TryGetFile("ptvsd\\__init__.py", GetType().Assembly);
            var ptvsdDest = PathUtils.GetAbsoluteDirectoryPath(projectDir, "ptvsd");
            if (File.Exists(ptvsdSource) && !Directory.Exists(ptvsdDest)) {
                Directory.CreateDirectory(ptvsdDest);
                var sourceDir = PathUtils.GetParent(ptvsdSource);
                foreach (var file in PathUtils.EnumerateFiles(sourceDir, pattern: "*.py" , fullPaths: false)) {
                    var destFile = PathUtils.GetAbsoluteFilePath(ptvsdDest, file);
                    if (!Directory.Exists(PathUtils.GetParent(destFile))) {
                        Directory.CreateDirectory(PathUtils.GetParent(destFile));
                    }
                    
                    File.Copy(PathUtils.GetAbsoluteFilePath(sourceDir, file), destFile, true);
                }

                projectItem.ContainingProject.ProjectItems.AddFromDirectory(PathUtils.TrimEndSeparator(ptvsdDest));
            }


            var webRoleSource = PythonToolsInstallPath.TryGetFile("Microsoft.PythonTools.WebRole.dll", GetType().Assembly);
            if (File.Exists(webRoleSource)) {
                projectItem.ContainingProject.ProjectItems.AddFromFileCopy(webRoleSource);
            }
        }

        public void RunFinished() { }

        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams
        ) {
            replacementsDictionary["$secret$"] = Path.GetRandomFileName().Replace(".", "");
        }

        public bool ShouldAddProjectItem(string filePath) {
            return true;
        }
    }
}
