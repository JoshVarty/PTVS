using System.Windows.Automation;

namespace TestUtilities.UI
{
    public class TextBox : AutomationWrapper
    {
        public TextBox(AutomationElement element)
            : base(element) { 
        }

        public string Value
        {
            get
            {
                return Element.GetTextPattern().DocumentRange.GetText(-1);
            }
            set
            {
                Element.GetValuePattern().SetValue(value);
            }
        }
    }
}
