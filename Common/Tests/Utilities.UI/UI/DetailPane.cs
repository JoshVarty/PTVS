using System.Windows.Automation;

namespace TestUtilities.UI
{
    public class DetailPane : TextBox
    {
        public DetailPane(AutomationElement element)
            : base(element) { 
        }
    }
}
