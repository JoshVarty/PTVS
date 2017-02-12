using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudioTools;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    /// <summary>
    /// Represents Current Working Directory node.
    /// </summary>
    internal class CurrentWorkingDirectoryNode : BaseSearchPathNode {

        public CurrentWorkingDirectoryNode(PythonProjectNode project, string path)
            : base(project, path, new VirtualProjectElement(project)) { }

        public override string Caption {
            get {
                return Strings.CurrentWorkingDirectoryCaption.FormatUI(base.Caption);
            }
        }

        //Working Directory node cannot be deleted
        internal override bool CanDeleteItem(Microsoft.VisualStudio.Shell.Interop.__VSDELETEITEMOPERATION deleteOperation) {
            return false;
        }

        public override int SortPriority {
            get {
                return CommonConstants.WorkingDirectorySortPriority;
            }
        }
    }
}
