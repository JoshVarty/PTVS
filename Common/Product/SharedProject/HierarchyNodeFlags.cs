using System;

namespace Microsoft.VisualStudioTools.Project {
    /// <summary>
    /// Represents various boolean states for the HiearchyNode
    /// </summary>
    [Flags]
    enum HierarchyNodeFlags {
        None,
        ExcludeFromScc = 0x01,
        IsExpanded = 0x02,
        HasParentNodeNameRelation = 0x04,
        IsVisible = 0x08
    }
}
