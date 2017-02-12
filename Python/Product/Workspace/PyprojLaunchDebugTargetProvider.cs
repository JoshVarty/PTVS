using System;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Project;
using Microsoft.PythonTools.Project.Web;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Workspace;
using Microsoft.VisualStudio.Workspace.Debug;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Workspace {
    [ExportLaunchDebugTarget(
        ProviderType,
        new[] { ".pyproj" }
    )]
    class PyprojLaunchDebugTargetProvider : ILaunchDebugTargetProvider {
        private const string ProviderType = "F2B8B667-3D13-4E51-B067-00C188D0EB7E";

        public const string LaunchTypeName = "pyproj";

        // Set by the workspace, not by our users
        internal const string ProjectKey = "target";

        public const string JsonSchema = @"{
  ""definitions"": {
    ""pyproj"": {
      ""type"": ""object"",
      ""properties"": {
        ""type"": {""type"": ""string"", ""enum"": [ ""pyproj"" ]}
      }
    },
    ""pyprojFile"": {
      ""allOf"": [
        { ""$ref"": ""#/definitions/default"" },
        { ""$ref"": ""#/definitions/pyproj"" }
      ]
    }
  },
    ""defaults"": {
        "".pyproj"": { ""$ref"": ""#/definitions/pyproj"" }
    },
    ""configuration"": ""#/definitions/pyprojFile""
}";

        public void LaunchDebugTarget(IWorkspace workspace, IServiceProvider serviceProvider, DebugLaunchActionContext debugLaunchActionContext) {
            var settings = debugLaunchActionContext.LaunchConfiguration;
            var moniker = settings.GetValue(ProjectKey, string.Empty);
            if (string.IsNullOrEmpty(moniker)) {
                throw new InvalidOperationException();
            }

            var solution = serviceProvider.GetService(typeof(SVsSolution)) as IVsSolution;
            var solution4 = solution as IVsSolution4;
            var debugger = serviceProvider.GetShellDebugger();

            if (solution == null || solution4 == null) {
                throw new InvalidOperationException();
            }

            solution4.EnsureSolutionIsLoaded(0);
            var proj = solution.EnumerateLoadedPythonProjects()
                .FirstOrDefault(p => string.Equals(p.GetMkDocument(), moniker, StringComparison.OrdinalIgnoreCase));

            if (proj == null) {
                throw new InvalidOperationException();
            }

            ErrorHandler.ThrowOnFailure(proj.GetLauncher().LaunchProject(true));
        }

        public bool SupportsContext(IWorkspace workspace, string filePath) {
            throw new NotImplementedException();
        }
    }
}
