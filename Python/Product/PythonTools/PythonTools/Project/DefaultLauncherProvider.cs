using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudioTools.Project;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools.Project {
    [Export(typeof(IPythonLauncherProvider))]
    class DefaultLauncherProvider : IPythonLauncherProvider2 {
        private readonly IServiceProvider _serviceProvider;
        private readonly PythonToolsService _pyService;
        internal const string DefaultLauncherName = "Standard Python launcher";

        [ImportingConstructor]
        public DefaultLauncherProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            _pyService = serviceProvider.GetPythonToolsService();
        }

        public IPythonLauncherOptions GetLauncherOptions(IPythonProject properties) {
            return new DefaultPythonLauncherOptions(properties);
        }

        public string Name {
            get {
                return DefaultLauncherName;
            }
        }

        public string LocalizedName {
            get {
                return Strings.DefaultLauncherName;
            }
        }

        public string Description {
            get {
                return Strings.DefaultLauncherDescription;
            }
        }

        public int SortPriority {
            get {
                return 0;
            }
        }

        public IProjectLauncher CreateLauncher(IPythonProject project) {
            return new DefaultPythonLauncher(_serviceProvider, project.GetLaunchConfigurationOrThrow());
        }
    }
}
