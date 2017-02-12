using System.Runtime.InteropServices;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [ComVisible(true)]
    public class CommonSearchPathContainerNodeProperties : NodeProperties {
        internal CommonSearchPathContainerNodeProperties(HierarchyNode node)
            : base(node) { }

        public override string GetClassName() {
            return Strings.SearchPathContainerProperties;
        }
    }
}
