using MSBuild = Microsoft.Build.Evaluation;

namespace TestUtilities.SharedProject {
    /// <summary>
    /// Base class for all generated project items.  Override Generate to create
    /// the item on disk (relative to the MSBuild.Project) and optionally add the
    /// generated item to the project.  
    /// </summary>
    public abstract class ProjectContentGenerator {
        /// <summary>
        /// Generates the specified item.  The item can use the project type to 
        /// customize the item.  The item can write it's self out to disk if 
        /// necessary and update the project file appropriately.
        /// </summary>
        public abstract void Generate(ProjectType projectType, MSBuild.Project project);
    }
}
