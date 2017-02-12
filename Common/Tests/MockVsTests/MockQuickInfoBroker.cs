using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;

namespace Microsoft.VisualStudioTools.MockVsTests {
    [Export(typeof(IQuickInfoBroker))]
    class MockQuickInfoBroker : IQuickInfoBroker {
        public IQuickInfoSession CreateQuickInfoSession(VisualStudio.Text.Editor.ITextView textView, VisualStudio.Text.ITrackingPoint triggerPoint, bool trackMouse) {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<IQuickInfoSession> GetSessions(VisualStudio.Text.Editor.ITextView textView) {
            throw new NotImplementedException();
        }

        public bool IsQuickInfoActive(VisualStudio.Text.Editor.ITextView textView) {
            throw new NotImplementedException();
        }

        public IQuickInfoSession TriggerQuickInfo(VisualStudio.Text.Editor.ITextView textView, VisualStudio.Text.ITrackingPoint triggerPoint, bool trackMouse) {
            throw new NotImplementedException();
        }

        public IQuickInfoSession TriggerQuickInfo(VisualStudio.Text.Editor.ITextView textView) {
            throw new NotImplementedException();
        }
    }
}
