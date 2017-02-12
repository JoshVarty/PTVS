using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Flavor;

namespace Microsoft.PythonTools.Django.Project {
    [Guid(DjangoProjectGuid)]
    public class DjangoProjectFactory : FlavoredProjectFactoryBase {
        internal const string DjangoProjectGuid = "5F0BE9CA-D677-4A4D-8806-6076C0FAAD37";
        private DjangoPackage _package;

        public DjangoProjectFactory(DjangoPackage package) {
            _package = package;
        }

        protected override object PreCreateForOuter(IntPtr outerProjectIUnknown) {
            var res = new DjangoProject();
            res._package = _package;
            return res;
        }
    }
}
