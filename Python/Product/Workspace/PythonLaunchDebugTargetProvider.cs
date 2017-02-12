using System;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Project;
using Microsoft.PythonTools.Project.Web;
using Microsoft.VisualStudio.Workspace;
using Microsoft.VisualStudio.Workspace.Debug;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Workspace {
    [ExportLaunchDebugTarget(
        ProviderType,
        new[] { PythonConstants.FileExtension, PythonConstants.WindowsFileExtension }
    )]
    class PythonLaunchDebugTargetProvider : ILaunchDebugTargetProvider {
        private const string ProviderType = "F2B8B667-3D13-4E51-B067-00C188D0EB7E";

        public const string LaunchTypeName = "python";

        // Set by the workspace, not by our users
        private const string ScriptNameKey = "target";

        public const string InterpreterKey = "interpreter";
        public const string InterpreterArgumentsKey = "interpreterArguments";
        public const string ScriptArgumentsKey = "scriptArguments";
        public const string WorkingDirectoryKey = "workingDirectory";
        public const string NativeDebuggingKey = "nativeDebug";
        public const string WebBrowserUrlKey = "webBrowserUrl";

        public const string DefaultInterpreterValue = "(default)";

        public const string JsonSchema = @"{
  ""definitions"": {
    ""python"": {
      ""type"": ""object"",
      ""properties"": {
        ""type"": {""type"": ""string"", ""enum"": [ ""python"" ]},
        ""interpreter"": { ""type"": ""string"" },
        ""interpreterArguments"": { ""type"": ""string"" },
        ""scriptArguments"": { ""type"": ""string"" },
        ""workingDirectory"": { ""type"": ""string"" },
        ""nativeDebug"": { ""type"": ""boolean"" },
        ""webBrowserUrl"": { ""type"": ""string"" }
      }
    },
    ""pythonFile"": {
      ""allOf"": [
        { ""$ref"": ""#/definitions/default"" },
        { ""$ref"": ""#/definitions/python"" }
      ]
    }
  },
    ""defaults"": {
        "".py"": { ""$ref"": ""#/definitions/python"" },
        "".pyw"": { ""$ref"": ""#/definitions/python"" }
    },
    ""configuration"": ""#/definitions/pythonFile""
}";

        public void LaunchDebugTarget(IWorkspace workspace, IServiceProvider serviceProvider, DebugLaunchActionContext debugLaunchActionContext) {
            var registry = serviceProvider.GetComponentModel().GetService<IInterpreterRegistryService>();

            var settings = debugLaunchActionContext.LaunchConfiguration;
            var scriptName = settings.GetValue(ScriptNameKey, string.Empty);
            var debug = !settings.GetValue("noDebug", false);
            var path = settings.GetValue(InterpreterKey, string.Empty);
            InterpreterConfiguration config = null;

            if (string.IsNullOrEmpty(scriptName)) {
                throw new InvalidOperationException(Strings.DebugLaunchScriptNameMissing);
            }

            if (!string.IsNullOrEmpty(path) && !DefaultInterpreterValue.Equals(path, StringComparison.OrdinalIgnoreCase)) {
                if (PathUtils.IsValidPath(path) && !Path.IsPathRooted(path)) {
                    // Cannot (currently?) get the workspace path easily from here, so we'll start from
                    // the startup file and work our way up until we find it.
                    var basePath = PathUtils.GetParent(scriptName);
                    string candidate = null;
                    
                    while (Directory.Exists(basePath)) {
                        candidate = PathUtils.GetAbsoluteFilePath(basePath, path);
                        if (File.Exists(candidate)) {
                            path = candidate;
                            break;
                        }
                        basePath = PathUtils.GetParent(basePath);
                    }
                }

                if (File.Exists(path)) {
                    config = registry.Configurations.FirstOrDefault(c => c.InterpreterPath.Equals(path, StringComparison.OrdinalIgnoreCase)) ??
                        new InterpreterConfiguration("Custom", path, PathUtils.GetParent(path), path);
                } else {
                    config = registry.FindConfiguration(path);
                }
            } else {
                var service = serviceProvider.GetComponentModel().GetService<IInterpreterOptionsService>();
                service.DefaultInterpreter.ThrowIfNotRunnable();
                config = service.DefaultInterpreter.Configuration;
                path = config.InterpreterPath;
            }

            if (!File.Exists(path)) {
                throw new InvalidOperationException(Strings.DebugLaunchInterpreterMissing_Path.FormatUI(path));
            }

            IProjectLauncher launcher = null;
            var launchConfig = new LaunchConfiguration(config) {
                InterpreterPath = config == null ? path : null,
                InterpreterArguments = settings.GetValue(InterpreterArgumentsKey, string.Empty),
                ScriptName = scriptName,
                ScriptArguments = settings.GetValue(ScriptArgumentsKey, string.Empty),
                WorkingDirectory = settings.GetValue(WorkingDirectoryKey, string.Empty),
                // TODO: Support search paths
                SearchPaths = null,
                // TODO: Support env variables
                Environment = null,
            };
            launchConfig.LaunchOptions[PythonConstants.EnableNativeCodeDebugging] = settings.GetValue(NativeDebuggingKey, false).ToString();


            var browserUrl = settings.GetValue(WebBrowserUrlKey, string.Empty);
            if (!string.IsNullOrEmpty(browserUrl)) {
                launchConfig.LaunchOptions[PythonConstants.WebBrowserUrlSetting] = browserUrl;
                launcher = new PythonWebLauncher(serviceProvider, launchConfig, launchConfig, launchConfig);
            }

            (launcher ?? new DefaultPythonLauncher(serviceProvider, launchConfig)).LaunchProject(debug);
        }

        public bool SupportsContext(IWorkspace workspace, string filePath) {
            throw new NotImplementedException();
        }
    }
}
