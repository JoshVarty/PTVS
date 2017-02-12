using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtilities.UI {
    public class CheckBox : AutomationWrapper {
        public string Name { get; set; }

        public CheckBox(AutomationElement element, CheckListView parent)
            : base(element) {
            Name = (string)Element.GetCurrentPropertyValue(AutomationElement.NameProperty);
        }

        public void SetSelected() {
            Assert.IsTrue((bool)Element.GetCurrentPropertyValue(AutomationElement.IsTogglePatternAvailableProperty), "Element is not a check box");
            TogglePattern pattern = (TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern);

            if (pattern.Current.ToggleState != ToggleState.On) pattern.Toggle();
            if (pattern.Current.ToggleState != ToggleState.On) pattern.Toggle();

            Assert.AreEqual(pattern.Current.ToggleState, ToggleState.On, "Could not toggle " + Name + " to On.");
        }

        public void SetUnselected() {
            Assert.IsTrue((bool)Element.GetCurrentPropertyValue(AutomationElement.IsTogglePatternAvailableProperty), "Element is not a check box");
            TogglePattern pattern = (TogglePattern)Element.GetCurrentPattern(TogglePattern.Pattern);

            if (pattern.Current.ToggleState != ToggleState.Off) pattern.Toggle();
            if (pattern.Current.ToggleState != ToggleState.Off) pattern.Toggle();
            Assert.AreEqual(pattern.Current.ToggleState, ToggleState.Off, "Could not toggle " + Name + " to Off.");
        }
    }
}
