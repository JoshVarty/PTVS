using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(ICompletionBroker))]
    class MockCompletionBroker : ICompletionBroker {
        private readonly IEnumerable<Lazy<ICompletionSourceProvider, IContentTypeMetadata>> _completionProviders;
        private readonly IIntellisenseSessionStackMapService _stackMap;

        [ImportingConstructor]
        public MockCompletionBroker(IIntellisenseSessionStackMapService stackMap, [ImportMany]IEnumerable<Lazy<ICompletionSourceProvider, IContentTypeMetadata>> completionProviders) {
            _stackMap = stackMap;
            _completionProviders = completionProviders;
        }

        public ICompletionSession CreateCompletionSession(ITextView textView, ITrackingPoint triggerPoint, bool trackCaret) {
            throw new NotImplementedException();
        }

        public void DismissAllSessions(ITextView textView) {
            foreach (var session in _stackMap.GetStackForTextView(textView).Sessions) {
                if (session is ICompletionSession) {
                    session.Dismiss();
                }
            }
        }

        public ReadOnlyCollection<ICompletionSession> GetSessions(ITextView textView) {
            List<ICompletionSession> res = new List<ICompletionSession>();
            foreach (var session in _stackMap.GetStackForTextView(textView).Sessions) {
                if (session is ICompletionSession) {
                    res.Add(session as ICompletionSession);
                }
            }
            return new ReadOnlyCollection<ICompletionSession>(res);
        }

        public bool IsCompletionActive(ITextView textView) {
            foreach (var session in _stackMap.GetStackForTextView(textView).Sessions) {
                if (session is ICompletionSession) {
                    return true;
                }
            }
            return false;
        }

        public ICompletionSession TriggerCompletion(ITextView textView, ITrackingPoint triggerPoint, bool trackCaret) {
            throw new NotImplementedException();
        }

        public ICompletionSession TriggerCompletion(ITextView textView) {
            ObservableCollection<CompletionSet> sets = new ObservableCollection<CompletionSet>();
            var session = new MockCompletionSession(
                textView,
                sets,
                textView.TextBuffer.CurrentSnapshot.CreateTrackingPoint(
                    textView.Caret.Position.BufferPosition.Position,
                    PointTrackingMode.Negative
                )
            );

            foreach (var provider in _completionProviders) {
                foreach (var targetContentType in provider.Metadata.ContentTypes) {
                    if (textView.TextBuffer.ContentType.IsOfType(targetContentType)) {
                        var source = provider.Value.TryCreateCompletionSource(textView.TextBuffer);
                        if (source != null) {
                            source.AugmentCompletionSession(session, sets);
                        }
                    }
                }
            }

            if (session.CompletionSets.Count > 0 && !session.IsDismissed) {
                _stackMap.GetStackForTextView(textView).PushSession(session);
            }

            return session;
        }
    }
}
