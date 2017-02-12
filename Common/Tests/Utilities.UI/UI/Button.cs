using System.Windows.Automation;

namespace TestUtilities.UI
{
    public class Button : AutomationWrapper
    {
        public Button(AutomationElement element)
            : base(element) { 
        }

        public void Click()
        {
            Invoke(Element);
        }
    }
}
