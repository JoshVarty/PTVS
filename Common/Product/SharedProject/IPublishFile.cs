namespace Microsoft.VisualStudioTools.Project {
    public interface IPublishFile {
        /// <summary>
        /// Returns the source file that should be copied from.
        /// </summary>
        string SourceFile {
            get;
        }

        /// <summary>
        /// Returns the relative path for the destination file.
        /// </summary>
        string DestinationFile {
            get;
        }
    }
}
