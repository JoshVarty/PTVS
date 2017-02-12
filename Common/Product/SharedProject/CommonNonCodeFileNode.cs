using System;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudioTools.Project {
    internal class CommonNonCodeFileNode : CommonFileNode {
        public CommonNonCodeFileNode(CommonProjectNode root, ProjectElement e)
            : base(root, e) {
        }


        /// <summary>
        /// Open a file depending on the SubType property associated with the file item in the project file
        /// </summary>
        protected override void DoDefaultAction() {
            if ("WebBrowser".Equals(SubType, StringComparison.OrdinalIgnoreCase)) {
                CommonPackage.OpenVsWebBrowser(ProjectMgr.Site, Url);
                return;
            }

            FileDocumentManager manager = this.GetDocumentManager() as FileDocumentManager;
            Utilities.CheckNotNull(manager, "Could not get the FileDocumentManager");

            Guid viewGuid = Guid.Empty;
            IVsWindowFrame frame;
            manager.Open(false, false, viewGuid, out frame, WindowFrameShowAction.Show);
        }

    }
}
