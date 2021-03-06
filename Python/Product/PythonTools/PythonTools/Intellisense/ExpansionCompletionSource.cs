using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Microsoft.PythonTools.Intellisense {
    class ExpansionCompletionSource {
        private readonly IServiceProvider _serviceProvider;
        private Task<IReadOnlyList<CompletionResult>> _snippets;

        public ExpansionCompletionSource(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            _snippets = GetAvailableSnippets();
        }

        public IEnumerable<CompletionResult> GetCompletions(int timeout) {
            var task = _snippets;
            if (task == null) {
                return null;
            }

            if (task.IsCompleted || task.Wait(timeout)) {
                return task.Result;
            }

            return null;
        }

        private async Task<IReadOnlyList<CompletionResult>> GetAvailableSnippets() {
            var textMgr = _serviceProvider.GetService(typeof(SVsTextManager)) as IVsTextManager2;
            IVsExpansionManager vsmgr;
            IExpansionManager mgr;
            if (textMgr == null || ErrorHandler.Failed(textMgr.GetExpansionManager(out vsmgr)) ||
                (mgr = vsmgr as IExpansionManager) == null) {
                return null;
            }

            try {
                var enumerator = await mgr.EnumerateExpansionsAsync(GuidList.guidPythonLanguageServiceGuid, 1, null, 0, 0, 0);
                if (enumerator == null) {
                    return null;
                }

                var res = new List<CompletionResult>();

                foreach (var e in COMEnumerable.ToList<VsExpansion>(enumerator.Next)) {
                    res.Add(new CompletionResult(
                        e.shortcut,
                        e.shortcut,
                        e.description,
                        PythonMemberType.CodeSnippet,
                        null
                    ));
                }

                return res;
            } catch (Exception ex) {
                Debug.Fail(ex.ToUnhandledExceptionMessage(GetType()));
                return null;
            }
        }
    }
}
