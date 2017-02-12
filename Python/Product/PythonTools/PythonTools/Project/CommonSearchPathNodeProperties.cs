using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudioTools;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [ComVisible(true)]
    [Guid(CommonConstants.SearchPathsPropertiesGuid)]
    public class CommonSearchPathNodeProperties : NodeProperties {
        #region properties
        [SRCategoryAttribute(SR.Misc)]
        [SRDisplayName(SR.FolderName)]
        [SRDescriptionAttribute(SR.FolderNameDescription)]
        [AutomationBrowsable(false)]
        public string FolderName {
            get {
                return PathUtils.GetFileOrDirectoryName(this.HierarchyNode.Url);
            }
        }

        [SRCategoryAttribute(SR.Misc)]
        [SRDisplayName(SR.FullPath)]
        [SRDescriptionAttribute(SR.FullPathDescription)]
        [AutomationBrowsable(true)]
        public string FullPath {
            get {
                return this.HierarchyNode.Url;
            }
        }

        #region properties - used for automation only
        [Browsable(false)]
        [AutomationBrowsable(true)]
        public string FileName {
            get {
                return this.HierarchyNode.Url;
            }
        }

        #endregion

        #endregion

        #region ctors
        internal CommonSearchPathNodeProperties(HierarchyNode node)
            : base(node) { }
        #endregion

        public override string GetClassName() {
            return Strings.SearchPathProperties;
        }
    }
}
