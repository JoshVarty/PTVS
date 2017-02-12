using System;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Commands {
    /// <summary>
    /// Provides the command for starting a file or the start item of a project in the REPL window.
    /// </summary>
    internal sealed class SurveyNewsCommand : Command {
        private readonly IServiceProvider _serviceProvider;

        public SurveyNewsCommand(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public override void DoCommand(object sender, EventArgs args) {
            _serviceProvider.GetPythonToolsService().SurveyNews.CheckSurveyNews(true);
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidSurveyNews; }
        }
    }
}
