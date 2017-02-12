using System;
using System.Windows.Automation;

namespace TestUtilities.UI.Python.Django {
    class NewAppDialog : AutomationDialog {
        public NewAppDialog(VisualStudioApp app, AutomationElement element)
            : base(app, element) {
        }

        public static NewAppDialog FromDte(VisualStudioApp app) {
            return new NewAppDialog(
                app,
                AutomationElement.FromHandle(
                    app.OpenDialogWithDteExecuteCommand("ProjectandSolutionContextMenus.Project.Add.Djangoapp")
                )
            );
        }

        public override void OK() {
            ClickButtonAndClose("_ok", nameIsAutomationId: true);
        }

        public override void Cancel() {
            ClickButtonAndClose("_cancel", nameIsAutomationId: true);
        }
        
        public string AppName {
            get {
                return GetAppNameEditBox().GetValuePattern().Current.Value;
            }
            set {
                GetAppNameEditBox().GetValuePattern().SetValue(value);
            }
        }

        private AutomationElement GetAppNameEditBox() {
            return Element.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "_newAppName")
            );
        }
    }
}
