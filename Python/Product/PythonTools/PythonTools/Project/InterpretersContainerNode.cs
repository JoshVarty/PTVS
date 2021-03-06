using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudioTools.Project;
using OleConstants = Microsoft.VisualStudio.OLE.Interop.Constants;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsMenus = Microsoft.VisualStudioTools.Project.VsMenus;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;

namespace Microsoft.PythonTools.Project {
    /// <summary>
    /// Python Environments container node.
    /// </summary>
    [ComVisible(true)]
    internal class InterpretersContainerNode : HierarchyNode {
        internal const string InterpretersNodeVirtualName = "Python Environments";
        private PythonProjectNode _projectNode;

        public InterpretersContainerNode(PythonProjectNode project)
            : base(project.ProjectMgr) {
            _projectNode = project;
            ExcludeNodeFromScc = true;
        }

        protected override NodeProperties CreatePropertiesObject() {
            return new InterpretersContainerNodeContainerNodeProperties(this);
        }

        /// <summary>
        /// Gets the default sort priority of this node.
        /// By default returns HierarchyNode. 
        /// </summary>
        public override int SortPriority {
            get {
                return PythonConstants.InterpretersContainerNodeSortPriority;
            }
        }

        public override int MenuCommandId {
            get { return PythonConstants.EnvironmentsContainerMenuId; }
        }

        public override Guid MenuGroupId {
            get { return GuidList.guidPythonToolsCmdSet; }
        }

        /// <summary>
        /// Gets the GUID of the item type of the node.
        /// By default returns System.Guid.Empty. 
        /// Implementations should return an item type GUID from the values listed in VSConstants. 
        /// </summary>
        public override Guid ItemTypeGuid {
            //A GUID constant used to specify that the type is a non-physical folder.
            get { return VSConstants.GUID_ItemType_VirtualFolder; }
        }

        /// <summary>
        /// Gets the absolute path for this node.
        /// </summary>
        public override string Url {
            // TODO: This node is not real - should we return null for Url?
            get { return InterpretersNodeVirtualName; }
        }

        /// <summary>
        /// Gets the caption of the hierarchy node.
        /// </summary>
        public override string Caption {
            get { return Strings.Environments; }
        }

        /// <summary>
        /// Disable inline editing of Caption of this node
        /// </summary>
        public override string GetEditLabel() {
            return null;
        }

        protected override bool SupportsIconMonikers {
            get { return true; }
        }

        protected override ImageMoniker GetIconMoniker(bool open) {
            // TODO: Update to PYEnvironment
            return KnownMonikers.DockPanel;
        }

        /// <summary>
        /// Interpreter container node cannot be dragged.
        /// </summary>
        protected internal override string PrepareSelectedNodesForClipBoard() {
            return null;
        }

        /// <summary>
        /// Interpreter container cannot be excluded.
        /// </summary>
        internal override int ExcludeFromProject() {
            return (int)OleConstants.OLECMDERR_E_NOTSUPPORTED;
        }

        internal override int QueryStatusOnNode(Guid cmdGroup, uint cmd, IntPtr pCmdText, ref QueryStatusResult result) {
            if (cmdGroup == VsMenus.guidStandardCommandSet97) {
                switch ((VsCommands)cmd) {
                    case VsCommands.Copy:
                    case VsCommands.Cut:
                        result |= QueryStatusResult.SUPPORTED | QueryStatusResult.INVISIBLE;
                        return VSConstants.S_OK;
                }
            } else if (cmdGroup == GuidList.guidPythonToolsCmdSet) {
                switch (cmd) {
                    case PythonConstants.AddEnvironment:
                    case PythonConstants.AddVirtualEnv:
                    case PythonConstants.AddExistingVirtualEnv:
                    case PythonConstants.ViewAllEnvironments:
                        result |= QueryStatusResult.SUPPORTED | QueryStatusResult.ENABLED;
                        return VSConstants.S_OK;
                }
            }

            return base.QueryStatusOnNode(cmdGroup, cmd, pCmdText, ref result);
        }

        /// <summary>
        /// Interpreter container node cannot be deleted.
        /// </summary>
        internal override bool CanDeleteItem(__VSDELETEITEMOPERATION deleteOperation) {
            return false;
        }

        /// <summary>
        /// Defines whether this node is valid node for painting the Search Paths icon.
        /// </summary>
        /// <returns></returns>
        protected override bool CanShowDefaultIcon() {
            return true;
        }

        public new PythonProjectNode ProjectMgr {
            get {
                return ((PythonProjectNode)base.ProjectMgr);
            }
        }
    }
}
