using System;
using System.Runtime.InteropServices;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Intellisense;
using Microsoft.VisualStudioTools;
using Microsoft.VisualStudioTools.Navigation;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Navigation {

    /// <summary>
    /// This interface defines the service that finds Python files inside a hierarchy
    /// and builds the informations to expose to the class view or object browser.
    /// </summary>
    [Guid(PythonConstants.LibraryManagerServiceGuid)]
    internal interface IPythonLibraryManager : ILibraryManager {
    }

    /// <summary>
    /// Implementation of the service that builds the information to expose to the symbols
    /// navigation tools (class view or object browser) from the Python files inside a
    /// hierarchy.
    /// </summary>
    [Guid(PythonConstants.LibraryManagerGuid)]
    internal class PythonLibraryManager : LibraryManager, IPythonLibraryManager {
        private readonly PythonToolsPackage/*!*/ _package;

        public PythonLibraryManager(PythonToolsPackage/*!*/ package)
            : base(package) {
            _package = package;
        }

        public override LibraryNode CreateFileLibraryNode(LibraryNode parent, HierarchyNode hierarchy, string name, string filename) {
            return new PythonFileLibraryNode(parent, hierarchy, hierarchy.Caption, filename);
        }

        protected override void OnNewFile(LibraryTask task) {
            if (IsNonMemberItem(task.ModuleID.Hierarchy, task.ModuleID.ItemID)) {
                return;
            }

            var analyzer = task.ModuleID.Hierarchy
                    .GetProject()
                    .GetPythonProject()
                    .GetAnalyzer();

            AnalysisEntry item;
            if (analyzer.GetAnalysisEntryFromPath(task.FileName) == null) {
                analyzer.AnalyzeFileAsync(task.FileName).ContinueWith(x => {
                    item = x.Result;

                    if (item != null) {
                        // We subscribe to OnNewAnalysis here instead of OnNewParseTree so that 
                        // in the future we can use the analysis to include type information in the
                        // object browser (for example we could include base type information with
                        // links elsewhere in the object browser).
                        item.AnalysisComplete += (sender, args) => {
                            _package.GetUIThread().InvokeAsync(() => FileParsed(task))
                                .HandleAllExceptions(_package, GetType())
                                .DoNotWait();
                        };
                    }
                });
            }
        }
    }
}
