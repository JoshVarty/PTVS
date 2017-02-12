using System;
using System.Windows.Automation;

namespace TestUtilities.UI {
    public class SelectCodeTypeDialog : AutomationWrapper {
        private CheckListView _availableCodeTypes;

        public SelectCodeTypeDialog(AutomationElement element) : base(element) {
            var actElement = Element.FindFirst(
                           TreeScope.Descendants,
                           new PropertyCondition(
                               AutomationElement.AutomationIdProperty,
                               "1005")); // AutomationId 1005 discovered with UISpy
            _availableCodeTypes = new CheckListView(actElement);
        }

        public SelectCodeTypeDialog(IntPtr hwnd) : this(AutomationElement.FromHandle(hwnd)) { }

        public CheckListView AvailableCodeTypes {
            get {
                return _availableCodeTypes;
            }
        }

        public void SetDebugSpecificCodeTypes() {
            Select(FindByAutomationId("1199")); // utomationId 1199 discovered with UISpy
        }

        public void SetAutomaticallyDetermineCodeTypes() {
            Select(FindByAutomationId("1196")); // AutomationId 1196
        }

        public CheckBox GetCodeTypeCheckBox(string codeType) {
            var selectedItem = _availableCodeTypes.GetFirstByName(codeType);
            return selectedItem;
        }

        public void ClickOk() {
            ClickButtonByName("OK");
        }

        public void ClickCancel() {
            ClickButtonByName("Cancel");
        }

    }
}
