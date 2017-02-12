using MSBuild = Microsoft.Build.Evaluation;

namespace TestUtilities.SharedProject {
    public class ProjectProperty : ProjectContentGenerator {
        public readonly string Name, Value;

        public ProjectProperty(string name, string value) {
            Name = name;
            Value = value;
        }

        public override void Generate(ProjectType projectType, MSBuild.Project project) {
            project.SetProperty(Name, Value);
        }
    }
}
