using System;
using System.Windows.Automation;

namespace TestUtilities.UI {
    class PythonLaunchWithErrorsDialog : AutomationWrapper {
        public PythonLaunchWithErrorsDialog(IntPtr hwnd)
            : base(AutomationElement.FromHandle(hwnd)) {
        }

        public void Yes() {
            Invoke(FindByName("Yes"));
        }

        public void No() {
            Invoke(FindByName("No"));
        }
    }
}
