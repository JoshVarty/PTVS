using System;
using Microsoft.Build.Construction;
using MSBuild = Microsoft.Build.Evaluation;

namespace TestUtilities.SharedProject {
    public class ImportDefinition : ProjectContentGenerator {
        public readonly string Project;
        
        public ImportDefinition(string project) {
            Project = project;
        }

        public override void Generate(ProjectType projectType, MSBuild.Project project) {
            var target = project.Xml.AddImport(Project);
        }
    }
}
