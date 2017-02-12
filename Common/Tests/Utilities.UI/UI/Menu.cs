using System.Collections.Generic;
using System.Windows.Automation;

namespace TestUtilities.UI
{
    class Menu : AutomationWrapper
    {
        public Menu(AutomationElement element)
            : base(element) {
        }

        public List<MenuItem> Items
        {
            get
            {
                Condition con = new PropertyCondition(
                                    AutomationElement.LocalizedControlTypeProperty,
                                    "menu item"
                                );
                AutomationElementCollection ell = Element.FindAll(TreeScope.Children, con);
                List<MenuItem> items = new List<MenuItem>();
                for (int i = 0; i < ell.Count; i++)
                {
                    items.Add(new MenuItem(ell[i]));
                }
                return items;
            }
        }
    }
}
