using System;
using System.Windows.Automation;

namespace TestUtilities.UI
{
    class MenuItem : AutomationWrapper
    {
        public MenuItem(AutomationElement element)
            : base(element) {
        }

        public string Value
        {
            get
            {
                return this.Element.Current.Name.ToString();
            }
        }

        public bool ToggleStatus
        {
            get
            {
                var pat = (TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern);
                if (pat.Current.ToggleState == ToggleState.On)
                    return true;
                return false;
            }
        }

        public void Check()
        {
            var pat = (TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern);
            if (pat.Current.ToggleState == ToggleState.Off)
            {
                try
                {
                    pat.Toggle();
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
        }

        public void Uncheck()
        {
            var pat = (TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern);
            if (pat.Current.ToggleState == ToggleState.On)
            {
                try
                {
                    pat.Toggle();
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
        }
    }
}
