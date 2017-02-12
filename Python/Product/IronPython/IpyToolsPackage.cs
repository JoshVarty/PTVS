using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.IronPythonTools {
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [Guid(IpyToolsPackageGuid)]
    [Description("Python - IronPython support")]
    [InstalledProductRegistration("#110", "#112", AssemblyVersionInfo.Version, IconResourceID = 400)]
    public sealed class IpyToolsPackage : Package {
        public const string IpyToolsPackageGuid = "af7eaf4b-5af3-3622-b39a-7ae7ed25e7b2";

        public IpyToolsPackage() {
        }
    }
}
