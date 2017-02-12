using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor.OptionsExtensionMethods;

namespace Microsoft.PythonTools.Intellisense {
    public static class CompletionSessionExtensions {
        public static CompletionOptions GetOptions(this ICompletionSession session, IServiceProvider serviceProvider) {
            var pyService = serviceProvider.GetPythonToolsService();

            var options = new CompletionOptions {
                ConvertTabsToSpaces = session.TextView.Options.IsConvertTabsToSpacesEnabled(),
                IndentSize = session.TextView.Options.GetIndentSize(),
                TabSize = session.TextView.Options.GetTabSize()
            };

            options.IntersectMembers = pyService.AdvancedOptions.IntersectMembers;
            options.HideAdvancedMembers = pyService.LangPrefs.HideAdvancedMembers;
            options.FilterCompletions = pyService.AdvancedOptions.FilterCompletions;
            options.SearchMode = pyService.AdvancedOptions.SearchMode;
            return options;
        }
    }

    class CompletionSource : ICompletionSource {
        private readonly ITextBuffer _textBuffer;
        private readonly CompletionSourceProvider _provider;

        public CompletionSource(CompletionSourceProvider provider, ITextBuffer textBuffer) {
            _textBuffer = textBuffer;
            _provider = provider;
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets) {
            var textBuffer = _textBuffer;
            var span = session.GetApplicableSpan(textBuffer);
            var triggerPoint = session.GetTriggerPoint(textBuffer);
            var options = session.GetOptions(_provider._serviceProvider);
            var provider = _provider._pyService.GetCompletions(
                session,
                session.TextView,
                textBuffer.CurrentSnapshot,
                span,
                triggerPoint,
                options
            );

            var completions = provider.GetCompletions(_provider._glyphService);
           
            if (completions != null && completions.Completions.Count > 0) {
                completionSets.Add(completions);
            }
        }

        public void Dispose() {
        }
    }
}
