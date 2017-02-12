using System.Windows.Automation;

namespace TestUtilities.UI {
    class PythonPerfExplorer : TreeView {
        public PythonPerfExplorer(AutomationElement element)
            : base(element) {
        }

    }
}
