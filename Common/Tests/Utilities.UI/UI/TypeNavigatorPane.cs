using System.Windows.Automation;

namespace TestUtilities.UI
{
    public class TypeNavigatorPane : TreeView
    {
        public TypeNavigatorPane(AutomationElement element)
            : base(element) { 
        }
    }
}
