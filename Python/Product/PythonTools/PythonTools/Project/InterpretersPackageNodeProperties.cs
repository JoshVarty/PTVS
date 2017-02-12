using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [ComVisible(true)]
    [Guid(PythonConstants.InterpretersPackagePropertiesGuid)]
    public class InterpretersPackageNodeProperties : NodeProperties {
        internal InterpretersPackageNodeProperties(HierarchyNode node)
            : base(node) { }

        [SRCategory(SR.Misc)]
        [SRDisplayName("PackageFullName")]
        [SRDescription("PackageFullNameDescription")]
        [AutomationBrowsable(true)]
        public string FullPath {
            get {
                return this.HierarchyNode.Url;
            }
        }

        public override string GetClassName() {
            return "Python Package Properties";
        }
    }
}
