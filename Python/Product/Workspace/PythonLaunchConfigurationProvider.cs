using Microsoft.VisualStudio.Workspace;
using Microsoft.VisualStudio.Workspace.Debug;

namespace Microsoft.PythonTools.Workspace {
    [ExportLaunchConfigurationProvider(
        ProviderType,
        new[] { PythonConstants.FileExtension, PythonConstants.WindowsFileExtension },
        PythonLaunchDebugTargetProvider.LaunchTypeName,
        PythonLaunchDebugTargetProvider.JsonSchema
    )]
    class PythonLaunchConfigurationProvider : ILaunchConfigurationProvider {
        public const string ProviderType = "CCA8088B-06BC-4AE7-8521-FC66628ABE13";

        public bool IsDebugLaunchActionSupported(DebugLaunchActionContext debugLaunchActionContext) {
            return true;
        }

        public void CustomizeLaunchConfiguration(DebugLaunchActionContext debugLaunchActionContext, IPropertySettings launchSettings) {
            launchSettings[PythonLaunchDebugTargetProvider.InterpreterKey] = "(default)";
            launchSettings[PythonLaunchDebugTargetProvider.InterpreterArgumentsKey] = string.Empty;
            launchSettings[PythonLaunchDebugTargetProvider.ScriptArgumentsKey] = string.Empty;
            launchSettings[PythonLaunchDebugTargetProvider.NativeDebuggingKey] = false;
            launchSettings[PythonLaunchDebugTargetProvider.WebBrowserUrlKey] = string.Empty;
        }
    }
}
