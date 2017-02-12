using System.Windows.Automation;

namespace TestUtilities.UI
{
    public class TypeBrowserPane : TreeView
    {
        public TypeBrowserPane(AutomationElement element)
            : base(element) {            
        }        
    }
}
