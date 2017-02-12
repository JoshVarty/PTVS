using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [ComVisible(true)]
    [Guid(PythonConstants.InterpretersPropertiesGuid)]
    public class InterpretersNodeProperties : NodeProperties {
        [Browsable(false)]
        [AutomationBrowsable(false)]
        protected IPythonInterpreterFactory Factory {
            get {
                var node = HierarchyNode as InterpretersNode;
                if (node != null) {
                    return node._factory;
                }
                return null;
            }
        }

        // TODO: Expose interpreter configuration through properties

        [SRCategory(SR.Misc)]
        [SRDisplayName(SR.FolderName)]
        [SRDescription(SR.FolderNameDescription)]
        [AutomationBrowsable(false)]
        public string FolderName {
            get {
                return PathUtils.GetFileOrDirectoryName(this.HierarchyNode.Url);
            }
        }

        [SRCategory(SR.Misc)]
        [SRDisplayName(SR.FullPath)]
        [SRDescription(SR.FullPathDescription)]
        [AutomationBrowsable(true)]
        public string FullPath {
            get {
                return this.HierarchyNode.Url;
            }
        }

#if DEBUG
        [SRCategory(SR.Misc)]
        [SRDisplayName("EnvironmentIdDisplayName")]
        [SRDescription("EnvironmentIdDescription")]
        [AutomationBrowsable(true)]
        public string Id => Factory?.Configuration?.Id ?? "";
#endif

        [SRCategory(SR.Misc)]
        [SRDisplayName("EnvironmentVersionDisplayName")]
        [SRDescription("EnvironmentVersionDescription")]
        [AutomationBrowsable(true)]
        public string Version => Factory?.Configuration?.Version.ToString() ?? "";

        [Browsable(false)]
        [AutomationBrowsable(true)]
        public string FileName => HierarchyNode.Url;

        internal InterpretersNodeProperties(HierarchyNode node)
            : base(node) { }

        public override string GetClassName() {
            return "Environment Properties";
        }

    }
}
