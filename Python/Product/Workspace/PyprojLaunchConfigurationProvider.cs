using System;
using System.IO;
using Microsoft.VisualStudio.Workspace;
using Microsoft.VisualStudio.Workspace.Debug;

namespace Microsoft.PythonTools.Workspace {
    [ExportLaunchConfigurationProvider(
        ProviderType,
        new[] { FileExtension },
        PyprojLaunchDebugTargetProvider.LaunchTypeName,
        PyprojLaunchDebugTargetProvider.JsonSchema
    )]
    class PyprojLaunchConfigurationProvider : ILaunchConfigurationProvider {
        public const string ProviderType = "CCA8088B-06BC-4AE7-8521-FC66628ABE13";

        private const string FileExtension = ".pyproj";

        public bool IsDebugLaunchActionSupported(DebugLaunchActionContext debugLaunchActionContext) {
            var settings = debugLaunchActionContext.LaunchConfiguration;
            var moniker = settings.GetValue(PyprojLaunchDebugTargetProvider.ProjectKey, string.Empty);
            if (string.IsNullOrEmpty(moniker) || !FileExtension.Equals(Path.GetExtension(moniker), StringComparison.OrdinalIgnoreCase)) {
                return false;
            }

            return true;
        }

        public void CustomizeLaunchConfiguration(DebugLaunchActionContext debugLaunchActionContext, IPropertySettings launchSettings) {
        }
    }
}
