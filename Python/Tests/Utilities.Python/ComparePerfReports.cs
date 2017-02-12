using System;
using System.Windows.Automation;

namespace TestUtilities.UI.Python {
    class ComparePerfReports : AutomationWrapper, IDisposable {
        public ComparePerfReports(IntPtr hwnd)
            : base(AutomationElement.FromHandle(hwnd)) {
            WaitForInputIdle();
        }

        public void Dispose() {
            object pattern;
            if (Element.TryGetCurrentPattern(WindowPattern.Pattern, out pattern)) {
                try {
                    ((WindowPattern)pattern).Close();
                } catch (ElementNotAvailableException) {
                }
            }
        }

        public void Ok() {
            WaitForInputIdle();
            WaitForClosed(TimeSpan.FromSeconds(10.0), () => ClickButtonByName("OK"));
        }

        public void Cancel() {
            WaitForInputIdle();
            WaitForClosed(TimeSpan.FromSeconds(10.0), () => ClickButtonByName("Cancel"));
        }

        public string ComparisonFile {
            get {
                return ComparisonFileTextBox.GetValue();
            }
            set {
                ComparisonFileTextBox.SetValue(value);
            }
        }

        private AutomationWrapper ComparisonFileTextBox {
            get {
                return new AutomationWrapper(FindByAutomationId("ComparisonFile"));
            }
        }

        public string BaselineFile {
            get {
                return BaselineFileTextBox.GetValue();
            }
            set {
                BaselineFileTextBox.SetValue(value);
            }
        }

        private AutomationWrapper BaselineFileTextBox {
            get {
                return new AutomationWrapper(FindByAutomationId("BaselineFile"));
            }
        }
    }
}
