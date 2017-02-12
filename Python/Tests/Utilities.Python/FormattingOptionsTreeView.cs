using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace TestUtilities.UI.Python {
    class FormattingOptionsTreeView : TreeView {
        public FormattingOptionsTreeView(AutomationElement element)
            : base(element) {
        }

        public static FormattingOptionsTreeView FromDialog(ToolsOptionsDialog dialog) {
            dialog.WaitForInputIdle();
            var spacingViewElement = dialog.FindByAutomationId("_optionsTree");
            for (int retries = 10; retries > 0 && spacingViewElement == null; retries -= 1) {
                Thread.Sleep(100);
                dialog.WaitForInputIdle();
                spacingViewElement = dialog.FindByAutomationId("_optionsTree");
            }

            return new FormattingOptionsTreeView(spacingViewElement);
        }
    }
}
