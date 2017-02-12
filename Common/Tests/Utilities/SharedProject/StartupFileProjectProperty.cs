namespace TestUtilities.SharedProject {
    /// <summary>
    /// Represents a project property for the startup file in a script
    /// based project system.  When generated the code extension is automatically
    /// appended.
    /// </summary>
    public sealed class StartupFileProjectProperty : ProjectProperty {
        public StartupFileProjectProperty(string filename)
            : base("StartupFile", filename) {
        }

        public override void Generate(ProjectType projectType, Microsoft.Build.Evaluation.Project project) {
            project.SetProperty(Name, Value + projectType.CodeExtension);
        }
    }
}
