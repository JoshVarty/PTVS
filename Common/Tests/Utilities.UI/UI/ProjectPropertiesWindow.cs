using System.Windows.Automation;
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtilities.UI
{
    class ProjectPropertiesWindow : AutomationWrapper
    {
        public ProjectPropertiesWindow(IntPtr element)
            : base(AutomationElement.FromHandle(element)) { 
        }

        public AutomationElement this[Guid tabGuid] {
            get {
                
                var tabItem = FindByAutomationId("PropPage_" + tabGuid.ToString("n").ToLower());
                Assert.IsNotNull(tabItem, "Failed to find page");
                
                AutomationWrapper.DumpElement(tabItem);
                foreach (var p in tabItem.GetSupportedPatterns()) {
                    Console.WriteLine("Supports {0}", p.ProgrammaticName);
                }

                try {
                    tabItem.GetInvokePattern().Invoke();
                } catch (InvalidOperationException) {
                    AutomationWrapper.DoDefaultAction(tabItem);
                }

                return FindByAutomationId("PageHostingPanel");
            }
        }
    }
}
