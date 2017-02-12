using System.Windows.Automation;

namespace TestUtilities.UI {
    public class ExceptionHelperDialog : AutomationWrapper {
        public ExceptionHelperDialog(AutomationElement element)
            : base(element) {
        }

        public string Title {
            get {
                // this is just the 1st child pane, and it's name is the same as the text it has.
                var exceptionNamePane = Element.FindFirst(
                    TreeScope.Children,
                    new PropertyCondition(
                        AutomationElement.ControlTypeProperty,
                        ControlType.Pane
                    )
                );

                return exceptionNamePane.Current.Name;
            }
        }

        public string Description {
            get {
                var desc = FindByName("Exception Description:");
                return (((TextPattern)desc.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.GetText(-1).ToString());
            }
            
        }

        public void Cancel() {
            ClickButtonByName("Cancel Button");
        }
    }
}
