using System;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Microsoft.PythonTools.ProjectWizards {
    static class WizardHelpers {
        public static IServiceProvider GetProvider(object automationObject) {
            var oleProvider = automationObject as IOleServiceProvider;
            if (oleProvider != null) {
                return new ServiceProvider(oleProvider);
            }
            MessageBox.Show(Strings.ErrorNoDte, Strings.ProductTitle);
            return null;
        }

        public static DTE GetDTE(object automationObject) {
            var dte = automationObject as DTE;
            if (dte == null) {
                var provider = GetProvider(automationObject);
                if (provider != null) {
                    dte = provider.GetService(typeof(DTE)) as DTE;
                }
            }
            if (dte == null) {
                MessageBox.Show(Strings.ErrorNoDte, Strings.ProductTitle);
            }
            return dte;
        }
    }
}
