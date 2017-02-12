
namespace Microsoft.VisualStudioTools.Project {
    /// <summary>
    /// Represents a project element which lives on disk and is visible when Show All Files
    /// is enabled.
    /// </summary>
    sealed class AllFilesProjectElement : VirtualProjectElement {
        private string _itemType;

        public AllFilesProjectElement(string path, string itemType, CommonProjectNode project)
            : base(project) {
            Rename(path);
        }

        public override bool IsExcluded {
            get {
                return true;
            }
        }

        public new CommonProjectNode ItemProject {
            get {
                return (CommonProjectNode)base.ItemProject;
            }
        }

        protected override string ItemType {
            get {
                return _itemType;
            }
            set {
                _itemType = value;
                OnItemTypeChanged();
            }
        }
    }
}
