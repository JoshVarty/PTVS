using System;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.PythonTools.Intellisense {
    internal class QuickInfoSource : IQuickInfoSource {
        private readonly ITextBuffer _textBuffer;
        private IQuickInfoSession _curSession;

        public QuickInfoSource(ITextBuffer textBuffer) {
            _textBuffer = textBuffer;
        }

        #region IQuickInfoSource Members

        public void AugmentQuickInfoSession(IQuickInfoSession session, System.Collections.Generic.IList<object> quickInfoContent, out ITrackingSpan applicableToSpan) {
            if (_curSession != null && !_curSession.IsDismissed) {
                _curSession.Dismiss();
                _curSession = null;
            }

            _curSession = session;
            _curSession.Dismissed += CurSessionDismissed;

            var quickInfo = GetQuickInfo(session.TextView);
            AugmentQuickInfoWorker(quickInfoContent, quickInfo, out applicableToSpan);
        }

        internal static void AugmentQuickInfoWorker(System.Collections.Generic.IList<object> quickInfoContent, QuickInfo quickInfo, out ITrackingSpan applicableToSpan) {
            if (quickInfo != null) {
                quickInfoContent.Add(quickInfo.Text);
                applicableToSpan = quickInfo.Span;
            } else {
                applicableToSpan = null;
            }
        }

        public static void AddQuickInfo(ITextView view, QuickInfo info) {
            view.Properties[typeof(QuickInfo)] = info;
        }

        private static QuickInfo GetQuickInfo(ITextView view) {
            QuickInfo quickInfo;
            if (view.Properties.TryGetProperty(typeof(QuickInfo), out quickInfo)) {
                return quickInfo;
            }
            return null;
        }

        private void CurSessionDismissed(object sender, EventArgs e) {
            _curSession = null;
        }        

        #endregion

        public void Dispose() {
        }
    }
}
