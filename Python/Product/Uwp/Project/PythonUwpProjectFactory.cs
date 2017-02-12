using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Flavor;

namespace Microsoft.PythonTools.Uwp.Project {
    [Guid(GuidList.guidUwpFactoryString)]
    public class PythonUwpProjectFactory : FlavoredProjectFactoryBase {
        private readonly PythonUwpPackage _package;

        public PythonUwpProjectFactory(PythonUwpPackage package) {
            _package = package;
        }

        protected override object PreCreateForOuter(IntPtr outerProjectIUnknown) {
            return new PythonUwpProject { Package = _package };
        }
    }
}
