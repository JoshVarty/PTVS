using System.Collections.Generic;
using Microsoft.VisualStudioTools.Parsing;

namespace Microsoft.VisualStudioTools.Navigation {
    interface IScopeNode {
        LibraryNodeType NodeType {
            get;
        }

        string Name {
            get;
        }

        string Description {
            get;
        }

        SourceLocation Start {
            get;
        }
        SourceLocation End {
            get;
        }

        IEnumerable<IScopeNode> NestedScopes {
            get;
        }
    }
}
