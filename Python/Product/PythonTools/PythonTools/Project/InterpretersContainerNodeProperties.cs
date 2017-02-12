using System.Runtime.InteropServices;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [ComVisible(true)]
    public class InterpretersContainerNodeContainerNodeProperties : NodeProperties {
        internal InterpretersContainerNodeContainerNodeProperties(HierarchyNode node)
            : base(node) { }

        public override string GetClassName() {
            return InterpretersContainerNode.InterpretersNodeVirtualName;
        }
    }
}
