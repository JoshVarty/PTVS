using System.Windows.Automation;

namespace TestUtilities.UI.Python {
    class PythonPerfToolBar : AutomationWrapper {
        public PythonPerfToolBar(AutomationElement element)
            : base(element) {
        }

        public void NewPerfSession() {
            ClickButtonByName("Add Performance Session");
        }

        public void LaunchSession() {
            ClickButtonByName("Start Profiling");
        }

        public void Stop() {
            var button = FindByName("Stop Profiling");
            for (int i = 0; i < 100 && !button.Current.IsEnabled; i++) {
                System.Threading.Thread.Sleep(100);
            }

            Invoke(button);
        }
    }
}
