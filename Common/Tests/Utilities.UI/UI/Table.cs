using System.Windows.Automation;

namespace TestUtilities.UI {
    public class Table : AutomationWrapper {
        private readonly GridPattern _pattern;

        public Table(AutomationElement element)
            : base(element) {
                _pattern = (GridPattern)element.GetCurrentPattern(GridPattern.Pattern);

        }

        public AutomationElement this[int row, int column] {
            get {
                return _pattern.GetItem(row, column);
            }
        }

        public AutomationElement FindItem(string name) {
            return Element.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, name));
        }
    }
}
