using System;
using System.ComponentModel.Composition;
using Microsoft.PythonTools;
using Microsoft.PythonTools.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.IronPythonTools.Debugger {
    [Export(typeof(IPythonLauncherProvider))]
    class IronPythonLauncherProvider : IPythonLauncherProvider2 {
        private readonly PythonToolsService _pyService;
        private readonly IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public IronPythonLauncherProvider([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            _pyService = (PythonToolsService)serviceProvider.GetService(typeof(PythonToolsService));
        }

        #region IPythonLauncherProvider Members

        public IPythonLauncherOptions GetLauncherOptions(IPythonProject properties) {
            return new IronPythonLauncherOptions(properties);
        }

        public string Name => "IronPython (.NET) launcher";

        public string LocalizedName => Strings.IronPythonLauncherName;

        public int SortPriority => 300;

        public string Description => Strings.IronPythonLauncherDescription;

        public IProjectLauncher CreateLauncher(IPythonProject project) {
            return new IronPythonLauncher(_serviceProvider, _pyService, project);
        }

        #endregion
    }
}
