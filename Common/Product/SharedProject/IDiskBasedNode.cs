namespace Microsoft.VisualStudioTools.Project {
    /// <summary>
    /// Represents a node which has a filename on disk, implemented by folder and file nodes.
    /// </summary>
    interface IDiskBasedNode {
        string Url {
            get;
        }

        void RenameForDeferredSave(string basePath, string baseNewPath);
    }
}
