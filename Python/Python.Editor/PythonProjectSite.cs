using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.LanguageServices.Implementation.ProjectSystem;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.LanguageServices.Implementation.TaskList;

namespace Python.Editor
{
    internal class PythonProjectSite : AbstractProject
    {
        public PythonProjectSite(VisualStudioProjectTracker projectTracker, 
            string projectName, 
            string projectFilePath,
            IVsHierarchy hierarchy,
            Guid projectGuid,
            IServiceProvider provider,
            VisualStudioWorkspaceImpl workspaceImpl,
            HostDiagnosticUpdateSource hostDiagnosticsUpdateSource
            ) 
            : base(projectTracker, null, projectName, projectFilePath, hierarchy, "Python", projectGuid, provider, workspaceImpl, hostDiagnosticsUpdateSource)
        {

        }
    }
}
