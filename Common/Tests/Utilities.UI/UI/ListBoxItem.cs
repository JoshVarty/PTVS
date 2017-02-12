using System.Windows.Automation;

namespace TestUtilities.UI {
    public class ListBoxItem : AutomationWrapper {
        private ListBox _parent;

        public ListBoxItem(AutomationElement element, ListBox parent) : base(element) { 
            _parent = parent;
        }
    }
}
