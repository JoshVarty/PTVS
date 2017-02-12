using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Flavor;

namespace Microsoft.PythonTools.Project.Web {
    [Guid(PythonConstants.WebProjectFactoryGuid)]
    internal class PythonWebProjectFactory : FlavoredProjectFactoryBase {
        private readonly IServiceProvider _site;

        internal PythonWebProjectFactory(IServiceProvider site) {
            _site = site;
        }

        protected override object PreCreateForOuter(IntPtr outerProjectIUnknown) {
            return new PythonWebProject(_site);
        }
    }
}
