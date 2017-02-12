namespace Microsoft.CookiecutterTools.Model {
    interface IProjectSystemClient {
        /// <summary>
        /// Returns information about the selected project node or folder node
        /// in solution explorer, or <c>null</c> if there is no selection or if the
        /// selection doesn't represent a valid folder.
        /// </summary>
        ProjectLocation GetSelectedFolderProjectLocation();

        /// <summary>
        /// Add the specified list of created folders and files to the specified
        /// project.
        /// </summary>
        /// <param name="location">
        /// The project to add to, and the path to the folder where the files
        /// were created within the project folder.
        /// </param>
        /// <param name="creationResult">
        /// Files that were created and must be added to the project.
        /// All paths are relative.
        /// </param>
        void AddToProject(ProjectLocation location, CreateFilesOperationResult creationResult);
    }

    class ProjectLocation {
        public string ProjectUniqueName { get; set; }
        public string ProjectKind { get; set; }
        public string FolderPath { get; set; }
    }
}
