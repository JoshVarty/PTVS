using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.PythonTools.Project;
using Microsoft.PythonTools.Project.Web;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Django.Debugger {
    [Export(typeof(IPythonLauncherProvider))]
    class DjangoLauncherProvider : IPythonLauncherProvider2 {
        internal readonly IEnumerable<Lazy<IPythonLauncherProvider>> _providers;

        [ImportingConstructor]
        public DjangoLauncherProvider([ImportMany]IEnumerable<Lazy<IPythonLauncherProvider>> allProviders) {
            _providers = allProviders;
        }

        #region IPythonLauncherProvider Members

        public IPythonLauncherOptions GetLauncherOptions(IPythonProject properties) {
            return new PythonWebLauncherOptions(properties);
        }

        public string Name {
            get { return "Django launcher"; }
        }

        public string LocalizedName {
            get { return Resources.DjangoLauncherName; }
        }

        public int SortPriority {
            get { return 200; }
        }

        public string Description {
            get { return Resources.DjangoLauncherDescription; }
        }

        public IProjectLauncher CreateLauncher(IPythonProject project) {
            var webLauncher = _providers.FirstOrDefault(p => p.Value.Name == PythonConstants.WebLauncherName);

            if (webLauncher == null) {
                throw new InvalidOperationException(Resources.CannotFindPythonWebLauncher);
            }

            return webLauncher.Value.CreateLauncher(project);
        }

        #endregion
    }
}
