using System;
using System.ComponentModel.Composition;
using Microsoft.PythonTools;
using Microsoft.PythonTools.Infrastructure;
using TestUtilities.SharedProject;
using MSBuild = Microsoft.Build.Evaluation;

namespace PythonToolsUITests {
    [Export(typeof(IProjectProcessor))]
    [ProjectExtension(".pyproj")]
    public class PythonProjectProcessor : IProjectProcessor {
        public void PreProcess(MSBuild.Project project) {
            project.SetProperty("ProjectHome", ".");
            project.SetProperty("WorkingDirectory", ".");

            var installPath = PathUtils.GetParent(PythonToolsInstallPath.GetFile("Microsoft.PythonTools.dll", typeof(PythonToolsPackage).Assembly));
            project.SetProperty("_PythonToolsPath", installPath);
            project.Xml.AddImport(Microsoft.PythonTools.Project.PythonProjectFactory.PtvsTargets);
        }

        public void PostProcess(MSBuild.Project project) {
        }
    }
}
