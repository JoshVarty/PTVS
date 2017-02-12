using System;
using System.Windows.Automation;

namespace TestUtilities.UI {
    class OpenProjectDialog : AutomationWrapper {
        public OpenProjectDialog(IntPtr hwnd)
            : base(AutomationElement.FromHandle(hwnd)) {
        }

        public void Open() {
            Invoke(FindButton("Open"));
        }

        public string ProjectName {
            get {
                var patterns = GetProjectNameBox().GetSupportedPatterns();
                var filename = (ValuePattern)GetProjectNameBox().GetCurrentPattern(ValuePattern.Pattern);
                return filename.Current.Value;
            }
            set {
                var patterns = GetProjectNameBox().GetSupportedPatterns();
                var filename = (ValuePattern)GetProjectNameBox().GetCurrentPattern(ValuePattern.Pattern);
                filename.SetValue(value);
            }
        }

        private AutomationElement GetProjectNameBox() {
            return Element.FindFirst(TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "File name:"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)
                )
            );
        }
    }
}
