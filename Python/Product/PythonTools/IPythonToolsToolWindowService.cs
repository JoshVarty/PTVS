using System;
using Microsoft.VisualStudio.Shell;

namespace Microsoft {
    interface IPythonToolsToolWindowService {
        void ShowWindowPane(Type windowType, bool focus);
        ToolWindowPane GetWindowPane(Type windowType, bool create);
    }
}
